using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.Messages.Events;
using NanoMessageBus;
using StorageAccess;
using Sample.ReadModel;
using Sample.Messages;
using Sample.ReadModel.People;
using Sample.Messages.Events.People;

namespace Sample.Denormalizer.People
{
    /// <summary>
    /// Event handler for the events published by a Person Aggregate from the Domain Model.
    /// Updates the read model using the read storage.
    /// </summary>
    public class PersonUpdater : 
        IHandleMessages<PersonCreated>,
        IHandleMessages<PersonMoved>,
        IHandleMessages<PersonDied>
    {
        private readonly IUpdateStorage storage;

        public PersonUpdater(IUpdateStorage storage)
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


        public void Handle(PersonDied message)
        {
            Person person = storage.Items<Person>().Where(p => p.Id == message.Id).Single();
            DeadPerson deadPerson = new DeadPerson
            {
                Id = person.Id,
                Name = person.Name
            };

            storage.Remove(person);
            storage.Add(deadPerson);
        }
    }
}
