using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sample.ReadModel;
using Sample.Messages.Commands;
using Sample.Messages;
using NanoMessageBus;
using StorageAccess;
using NanoMessageBus.Core;

namespace Sample.Client.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly IQueryStorage store;
        private readonly ISendMessages bus;

        public PersonController(IQueryStorage store, ISendMessages bus)
        {
            this.store = store;
            this.bus = bus;
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Person> persons = store.Items<Person>();
            
            return View(persons);
        }

        [HttpGet]
        public ActionResult AddressChanges()
        {
            IEnumerable<AddressChanges> changes = store.Items<AddressChanges>();
            return View(changes);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Person person = new Person();
            return View(person);
        }

        [HttpPost]
        public ActionResult Create(Person person)
        {
            person.Id = Guid.NewGuid();
            CreatePerson command = new CreatePerson(person.Id, person.Name, person.Street, person.StreetNumber);
            bus.Send(command);
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Move(Guid id)
        {
            Person person = store.Items<Person>().Where(p => p.Id == id).Single();
            return View(person);
        }

        [HttpPost]
        public ActionResult Move(Person person)
        {
            MovePerson command = new MovePerson(person.Id, person.Street, person.StreetNumber);
            bus.Send(command);
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Kill(Guid id) 
        {
            // it's not recomanded to delete/nuke data using GET requests.
            // you should require the user to POST the id instead in real systems

            KillPerson command = new KillPerson(id);
            bus.Send(command);
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeadPersons()
        {
            IEnumerable<DeadPerson> persons = store.Items<DeadPerson>();

            return View(persons);
        }
    }
}
