using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Events
{
    public class PersonDied : IEvent
    {
        public PersonDied(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get;private set; }
    }
}
