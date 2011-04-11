using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sample.Messages.Events.People
{
    public class PersonCreated : IEvent
    {
        public readonly Guid AggregateId;
        public readonly string Name;
        public readonly string Street;
        public readonly string StreetNumber;

        public PersonCreated(Guid aggregateId, string name, string street, string streetNumber)
        {
            this.AggregateId = aggregateId;
            this.Name = name;
            this.Street = street;
            this.StreetNumber = streetNumber;
        }
    }
}
