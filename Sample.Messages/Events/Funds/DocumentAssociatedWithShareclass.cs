using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Events.Funds
{
    public class DocumentAssociatedWithShareclass : IEvent
    {
        public readonly Guid AggregateId;
        public readonly Guid ShareClassId;
        public readonly string ShareClassType;

        public DocumentAssociatedWithShareclass(Guid documentId, Guid shareClassId, string shareClassType)
        {
            this.AggregateId = documentId;
            this.ShareClassId = shareClassId;
            this.ShareClassType = shareClassType;
        }        
    }
}
