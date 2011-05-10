using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Events.Funds
{
    public sealed class DocumentCreated : IEvent
    {
        public readonly Guid Id;
        public readonly string AccessionNumber;

        public DocumentCreated(Guid id, string accessionNumber)
        {
            this.Id = id;
            this.AccessionNumber = accessionNumber;
        }        
    }
}
