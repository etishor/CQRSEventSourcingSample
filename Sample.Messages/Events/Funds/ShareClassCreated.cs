using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Events.Funds
{
    public class ShareClassCreated : IEvent
    {
        public ShareClassCreated(Guid id, string ticker, string type)
        {
            this.AggregateId = id;
            this.Ticker = ticker;
            this.Type = type;
        }

        public Guid AggregateId { get; set; }
        public string Ticker { get; set; }
        public string Type { get; set; }
    }
}
