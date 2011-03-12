using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.Messages;
using CommonDomain.Core;
using Sample.Messages.Events.Funds;

namespace Sample.DomainModel.Funds
{
    public class ShareClass : AggregateBase<IEvent>
    {
        private Ticker ticker;
        private ShareClassType type;

        private ShareClass(Guid id)
        {
            this.Id = id;
        }

        public ShareClass(Guid id, Ticker ticker, ShareClassType type)
            :this(id)
        {
            RaiseEvent(new ShareClassCreated(id, ticker.Symbol , type.Name.ToString()));
        }

        private void Apply(ShareClassCreated @event)
        {
            this.ticker = new Ticker(@event.Ticker);
            this.type = ShareClassType.CreateFromString(@event.Type);
        }
    }
}
