using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Events.People
{
    public class PersonCreated : IEvent
    {
        public PersonCreated(Guid id, string name, string street, string streetNumber)
        {
            this.AggregateId = id;
            this.Name = name;
            this.Street = street;
            this.StreetNumber = streetNumber;
        }

        public Guid AggregateId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
    }
}
