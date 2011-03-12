using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonDomain.Core;
using Sample.Messages;
using Sample.Messages.Events;
using Sample.Messages.Events.People;

namespace Sample.DomainModel.People
{
    /// <summary>
    /// Sample aggregate root in the domain. 
    /// </summary>
    public class Person : AggregateBase<IEvent>
    {
        private PersonName name;
        private Address currentAddress;
        private bool isAlive;

        /// <summary>
        /// Infrastructure constructor.
        /// </summary>
        private Person(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="name">The name.</param>
        /// <param name="address">The address.</param>
        public Person(Guid id , PersonName name, Address address)
        {
            this.Id = id;
            // the event will be routed by convention to a method called ApplyEvent(type of event)
            RaiseEvent(new PersonCreated(id, name.Value,address.Street,address.Number));
        }

        /// <summary>
        /// Factory method for creating person objects.
        /// </summary>
        public static Person CreatePerson(Guid id, string name, string street, string streetNumber)
        {
            // in some cases the creation process is best delegated to a factory method.
            return new Person(id, new PersonName(name), new Address(street, streetNumber));
        }

        /// <summary>
        /// Moves this person to a new address.
        /// </summary>
        /// <param name="newAddress">The new address.</param>
        public void MoveToAddress(Address newAddress)
        {
            if (!this.isAlive)
            {
                throw new InvalidOperationException("Dead persons can't move.");
            }

            if (newAddress == currentAddress)
            {
                throw new InvalidOperationException("The new address must be different from the current one.");
            }

            RaiseEvent(new PersonMoved(Id, currentAddress.Street, currentAddress.Number, newAddress.Street, newAddress.Number));
        }

        /// <summary>
        /// Kills this person.
        /// </summary>
        public void Kill()
        {
            if (!this.isAlive)
            {
                throw new InvalidOperationException("Dead persons can't die again.");
            }

            RaiseEvent(new PersonDied(Id));
        }
        
        /// <summary>
        /// Applies the event. This method can be called when an aggregate method does RaiseEvent or 
        /// when the infrastructure loads the aggregate from the event stream.
        /// </summary>
        /// <param name="event">The @event.</param>
        private void Apply(PersonCreated @event)
        {
            // in the apply event handlers we should only have property assignements
            this.name = new PersonName(@event.Name);
            this.currentAddress = new Address(@event.Street, @event.StreetNumber);
            this.isAlive = true;
        }

        private void Apply(PersonMoved @event)
        {
            this.currentAddress = new Address(@event.NewStreet, @event.NewNumber);
        }

        private void Apply(PersonDied @event)
        {
            this.isAlive = false;
        }
    }
}
