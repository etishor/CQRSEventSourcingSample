using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Commands
{
    public class MovePerson
    {
        public MovePerson(Guid personId, string street, string streetNumber)
        {
            this.PersonId = personId;
            this.Street = street;
            this.StreetNumber = streetNumber;
        }

        public Guid PersonId { get; private set; }
        public string Street { get; private set; }
        public string StreetNumber { get; private set; }
    }
}
