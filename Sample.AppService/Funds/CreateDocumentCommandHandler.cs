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
    public class CreateDocumentCommandHandler : IHandleMessages<CreateDocument>
    {
        private readonly IRepository repository;

        public CreateDocumentCommandHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(CreateDocument message)
        {
            Console.WriteLine(message.AccessionNumber);
            Document document = new Document(message.Id, new AccessionNumber(message.AccessionNumber));
            repository.Save(document, Guid.NewGuid(), null);
        }
    }
}
