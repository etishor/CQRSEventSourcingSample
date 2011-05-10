using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.Messages.Commands.Funds;
using NanoMessageBus;
using CommonDomain.Persistence;
using Sample.DomainModel.Funds;

namespace Sample.AppService.Funds
{
    public class AssociateShareClassToDocumentCommandHandler : IHandleMessages<AssociateShareClassToDocument>
    {
        private readonly IRepository repository;

        public AssociateShareClassToDocumentCommandHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(AssociateShareClassToDocument message)
        {
            Document document = repository.GetById<Document>(message.DocumentId, int.MaxValue);

            DocumentShareClassAssociation association =
                new DocumentShareClassAssociation(document.Id, message.ShareClassId, ShareClassType.CreateFromString(message.ShareClassType));

            document.AssociateWithShareClass(association);

            repository.Save(document, Guid.NewGuid(), null);
        }
    }
}
