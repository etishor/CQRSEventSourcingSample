using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Events.Funds
{
    public class ShareClassCreated : IEvent
    {
        public readonly Guid AggregateId;
        public readonly string Ticker;
        public readonly string Type;

        public ShareClassCreated(Guid id, string ticker, string type)
        {
            this.AggregateId = id;
            this.Ticker = ticker;
            this.Type = type;
        }       
    }
}
