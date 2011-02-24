using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.Messages.Events;
using NanoMessageBus;
using StorageAccess;
using Sample.ReadModel;
using Sample.Messages;

namespace Sample.Denormalizer
{
    public class PersonEventsHandler : 
        IHandleMessages<PersonCreated>,
        IHandleMessages<PersonMoved>,
        IHandleMessages<IEvent>
    {
        private readonly IUpdateStorage storage;

        public PersonEventsHandler(IUpdateStorage storage)
        {
            this.storage = storage;
        }

        public void Handle(PersonCreated message)
        {
            Person person = new Person
            {
                Id = message.Id,
                Name = message.Name,
                Street = message.Street,
                StreetNumber = message.StreetNumber
            };
            storage.Add(person);
        }

        public void Handle(PersonMoved message)
        {
            Person person = storage.Items<Person>().Where(p => p.Id == message.Id).Single();

            person.Street = message.NewStreet;
            person.StreetNumber = message.NewNumber;

            storage.Update(person);
        }

        public void Handle(IEvent message)
        {
            Console.WriteLine(message.Id.ToString());
        }
    }
}
