using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sample.Messages.Events.People
{
    public sealed class PersonCreated : IEvent
    {
        public readonly Guid Id;
        public readonly string Name;
        public readonly string Street;
        public readonly string StreetNumber;

        public PersonCreated(Guid id, string name, string street, string streetNumber)
        {
            this.Id = id;
            this.Name = name;
            this.Street = street;
            this.StreetNumber = streetNumber;
        }
    }
}
