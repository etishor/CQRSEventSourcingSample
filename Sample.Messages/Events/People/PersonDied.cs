using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Events.People
{
    public class PersonDied : IEvent
    {
        public PersonDied(Guid id)
        {
            this.AggregateId = id;
        }

        public Guid AggregateId { get;set; }
    }
}
