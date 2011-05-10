using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.ReadModel.People
{
    /// <summary>
    /// Read Model object for displaying a Person.
    /// Denormalized form of the Aggregate in the DomainModel.
    /// </summary>
    public class Person : ReadModelEntity
    {
        public Person() { }// needed for asp.net model binder
        public Person(Guid id) : base(id) { }

        public string Name { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
    }
}
