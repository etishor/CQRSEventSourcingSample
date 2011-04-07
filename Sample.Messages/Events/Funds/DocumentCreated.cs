using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Events.Funds
{
    public class DocumentCreated : IEvent
    {
        public readonly Guid AggregateId;
        public readonly string AccessionNumber;

        public DocumentCreated(Guid id, string accessionNumber)
        {
            this.AggregateId = id;
            this.AccessionNumber = accessionNumber;
        }        
    }
}
