using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.Messages.Events;
using NanoMessageBus;
using StorageAccess;
using Sample.ReadModel;

namespace Sample.Denormalizer
{
    public class AddressChangesUpdater: IHandleMessages<PersonMoved>
    {
        private readonly IUpdateStorage storage;

        public AddressChangesUpdater(IUpdateStorage storage)
        {
            this.storage = storage;
        }

        public void Handle(PersonMoved message)
        {
            storage.Add(new AddressChanges
            {
                NewNumber = message.NewNumber,
                NewStreet = message.NewStreet,
                OldNumber = message.OldNumber,
                OldStreet = message.OldStreet
            });
        }
    }
}
