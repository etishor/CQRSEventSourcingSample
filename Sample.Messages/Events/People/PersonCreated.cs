using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Events.People
{
    public class PersonCreated : IEvent
    {
        public readonly Guid AggregateId;
        public readonly string Name;
        public readonly string Street;
        public readonly string StreetNumber;

        public PersonCreated(Guid id, string name, string street, string streetNumber)
        {
            this.AggregateId = id;
            this.Name = name;
            this.Street = street;
            this.StreetNumber = streetNumber;
        }
    }
}
