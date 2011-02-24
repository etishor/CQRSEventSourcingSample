using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.DomainModel
{
    public class Address
    {
        public Address(string street, string number)
        {
            this.Street = street;
            this.Number = number;
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
    }
}
