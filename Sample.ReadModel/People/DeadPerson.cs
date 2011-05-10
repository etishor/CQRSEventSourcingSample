using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.ReadModel.People
{
    public class DeadPerson : ReadModelEntity
    {
        public DeadPerson(Guid id) : base(id) { }

        public string Name { get; set; }
    }
}
