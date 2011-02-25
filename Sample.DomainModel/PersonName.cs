using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.DomainModel
{
    /// <summary>
    /// Value object representing the name of the person
    /// </summary>
    public class PersonName
    {
        public PersonName(string name)
        {
            this.Value = name;
        }

        public string Value { get; private set; }
    }
}
