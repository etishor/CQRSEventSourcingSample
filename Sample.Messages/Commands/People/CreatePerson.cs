using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Commands.People
{
    public class CreatePerson 
    {
        public CreatePerson(Guid id, string name, string street, string streetNumber)
        {
            this.Id = id;
            this.Name = name;
            this.Street = street;
            this.StreetNumber = streetNumber;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
    }
}
