using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Events.People
{
    public class PersonMoved : IEvent
    {
        public PersonMoved(Guid id, string oldStreet, string oldNumber, string newStreet, string newNumber)
        {
            this.AggregateId = id;
            this.OldStreet = oldStreet;
            this.OldNumber = oldNumber;
            this.NewStreet = newStreet;
            this.NewNumber = newNumber;
        }

        public Guid AggregateId { get; set; }

        public string OldStreet { get; set; }
        public string OldNumber { get; set; }

        public string NewStreet { get; set; }
        public string NewNumber { get; set; }
    }
}
