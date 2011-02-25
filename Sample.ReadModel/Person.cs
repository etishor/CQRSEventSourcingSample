using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.ReadModel
{
    /// <summary>
    /// Read Model object for displaying a Person.
    /// Denormalized form of the Aggregate in the DomainModel.
    /// </summary>
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
    }
}
