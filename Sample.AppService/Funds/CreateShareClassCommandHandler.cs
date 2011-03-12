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
    public class CreateShareClassCommandHandler : IHandleMessages<CreateShareClass>
    {
        private readonly IRepository repository;

        public CreateShareClassCommandHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(CreateShareClass message)
        {
            ShareClass share = new ShareClass(message.Id, new Ticker(message.Ticker), ShareClassType.CreateFromString(message.Type));

            repository.Save(share, Guid.NewGuid(), null);
        }
    }
}
