using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonDomain.Core;
using Sample.Messages;
using Sample.Messages.Events;

namespace Sample.DomainModel
{
    public class Person : AggregateBase<IEvent>
    {
        private PersonName name;
        private Address currentAddress;        

        public Person(Guid id , PersonName name, Address address)
        {
            this.Id = id;
            RaiseEvent(new PersonCreated(id, name.Value,address.Street,address.Number));
        }

        public void MoveToAddress(Address newAddress)
        {
            RaiseEvent(new PersonMoved(Id, currentAddress.Street, currentAddress.Number, newAddress.Street, newAddress.Number));
        }

        private void ApplyEvent(PersonCreated @event)
        {
            this.name = new PersonName(@event.Name);
            this.currentAddress = new Address(@event.Street, @event.StreetNumber);
        }

        private void ApplyEevent(PersonMoved @event)
        {
            this.currentAddress = new Address(@event.NewStreet, @event.NewNumber);
        }
    }
}
