using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.Messages.Events.Funds;
using NanoMessageBus;
using StorageAccess;
using Sample.ReadModel.Funds;

namespace Sample.Denormalizer.Funds
{
    public class ShareClassUpdater : IHandleMessages<ShareClassCreated>
    {
        private readonly IUpdateStorage storage;

        public ShareClassUpdater(IUpdateStorage storage)
        {
            this.storage = storage;
        }

        public void Handle(ShareClassCreated message)
        {
            ShareClass share = new ShareClass(message.Id)
            {
                Ticker = message.Ticker,
                Type = message.Type
            };

            storage.Add(share);
        }
    }
}
