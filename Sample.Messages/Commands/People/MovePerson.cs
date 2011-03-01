using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Commands.People
{
    public class MovePerson
    {
        public MovePerson(Guid personId, string street, string streetNumber)
        {
            this.PersonId = personId;
            this.Street = street;
            this.StreetNumber = streetNumber;
        }

        public Guid PersonId { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
    }
}
