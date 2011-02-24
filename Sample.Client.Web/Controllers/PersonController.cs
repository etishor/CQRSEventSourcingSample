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
        
        public ActionResult Index()
        {
            IEnumerable<Person> persons = store.Items<Person>();
            
            return View(persons);
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

    }
}
