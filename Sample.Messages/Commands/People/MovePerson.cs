using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Commands.People
{
    public sealed class MovePerson
    {
        public readonly Guid PersonId;
        public readonly string Street;
        public readonly string StreetNumber;

        public MovePerson(Guid personId, string street, string streetNumber)
        {
            this.PersonId = personId;
            this.Street = street;
            this.StreetNumber = streetNumber;
        }
    }
}
