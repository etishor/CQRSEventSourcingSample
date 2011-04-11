using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Events.People
{
    public class PersonDied : IEvent
    {
        public readonly Guid Id;

        public PersonDied(Guid id)
        {
            this.Id = id;
        }
    }
}
