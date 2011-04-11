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
            Document document = new Document
            {
                Id = message.Id,
                AccessionNumber = message.AccessionNumber,
                ShareClasses = new List<ShareClass>()
            };
            storage.Add(document);
        }

        public void Handle(DocumentAssociatedWithShareclass message)
        {
            Document document = storage.Items<Document>().Where(d => d.Id == message.DocumentId).Single();

            document.ShareClasses.Add(new ShareClass
            {
                Id = message.ShareClassId,
                Type = message.ShareClassType,
                // TODO: not sure if this is the best option
                Ticker = storage.Items<ShareClass>().Where(s => s.Id == message.ShareClassId).Select(s => s.Ticker).Single()
            });

            storage.Update(document);
        }
    }
}
