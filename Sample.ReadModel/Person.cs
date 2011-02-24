using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.ReadModel
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
    }
}
