using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Commands.People
{
    public sealed class CreatePerson
    {
        public readonly Guid Id;
        public readonly string Name;
        public readonly string Street;
        public readonly string StreetNumber;

        public CreatePerson(Guid id, string name, string street, string streetNumber)
        {
            this.Id = id;
            this.Name = name;
            this.Street = street;
            this.StreetNumber = streetNumber;
        }
    }
}
