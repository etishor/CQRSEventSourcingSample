using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Events.People
{
    public class PersonMoved : IEvent
    {
        public readonly Guid Id;

        public readonly string OldStreet;
        public readonly string OldNumber;

        public readonly string NewStreet;
        public readonly string NewNumber;

        public PersonMoved(Guid id, string oldStreet, string oldNumber, string newStreet, string newNumber)
        {
            this.Id = id;
            this.OldStreet = oldStreet;
            this.OldNumber = oldNumber;
            this.NewStreet = newStreet;
            this.NewNumber = newNumber;
        }
    }
}
