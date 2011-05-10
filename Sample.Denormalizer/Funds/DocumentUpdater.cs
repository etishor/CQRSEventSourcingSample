using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.Messages.Events.Funds;
using NanoMessageBus;
using StorageAccess;
using Sample.ReadModel.Funds;
using Sample.ReadModel;

namespace Sample.Denormalizer.Funds
{
    public class DocumentUpdater : 
        IHandleMessages<DocumentCreated>,
        IHandleMessages<DocumentAssociatedWithShareclass>
    {
        private readonly IUpdateStorage storage;

        public DocumentUpdater(IUpdateStorage storage)
        {
            this.storage = storage;
        }

        public void Handle(DocumentCreated message)
        {
            Document document = new Document(message.Id)
            {
                AccessionNumber = message.AccessionNumber,
                ShareClasses = new List<ShareClass>()
            };
            storage.Add(document);
        }

        public void Handle(DocumentAssociatedWithShareclass message)
        {
            Document document = storage.Load<Document>(message.DocumentId);

            document.ShareClasses.Add(new ShareClass(message.ShareClassId)
            {
                Type = message.ShareClassType,
                // TODO: not sure if this is the best option
                Ticker = storage.Load<ShareClass>(message.ShareClassId).Ticker
            });

            storage.Update(document);
        }
    }
}
