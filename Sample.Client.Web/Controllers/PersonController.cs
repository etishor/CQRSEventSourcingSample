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
using Sample.ReadModel.People;
using Sample.Messages.Commands.People;

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
            Person person = new Person(Guid.NewGuid());
            return View(person);
        }

        [HttpPost]
        public ActionResult Create(Person person)
        {
            CreatePerson command = new CreatePerson(Guid.NewGuid(), person.Name, person.Street, person.StreetNumber);
            bus.Send(command);
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Move(Guid id)
        {
            Person person = store.Load<Person>(id);
            return View(person);
        }

        [HttpPost]
        public ActionResult Move(Person person)
        {
            MovePerson command = new MovePerson(person.AggregateId, person.Street, person.StreetNumber);
            bus.Send(command);
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Kill(Guid id) 
        {
            // it's not recommended to delete/nuke data using GET requests.
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
