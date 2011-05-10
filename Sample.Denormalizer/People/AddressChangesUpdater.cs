using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.Messages.Events;
using NanoMessageBus;
using StorageAccess;
using Sample.ReadModel;
using Sample.ReadModel.People;
using Sample.Messages.Events.People;

namespace Sample.Denormalizer.People
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
            storage.Add(new AddressChanges(message.Id)
            {
                NewNumber = message.NewNumber,
                NewStreet = message.NewStreet,
                OldNumber = message.OldNumber,
                OldStreet = message.OldStreet
            });
        }
    }
}
