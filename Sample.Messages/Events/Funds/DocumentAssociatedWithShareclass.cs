using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Events.Funds
{
    public class DocumentAssociatedWithShareclass : IEvent
    {
        public DocumentAssociatedWithShareclass(Guid documentId, Guid shareClassId, string shareClassType)
        {
            this.AggregateId = documentId;
            this.ShareClassId = shareClassId;
            this.ShareClassType = shareClassType;
        }

        public Guid AggregateId {get; set;}
        public Guid ShareClassId { get; set; }
        public string ShareClassType { get; set; }
    }
}
