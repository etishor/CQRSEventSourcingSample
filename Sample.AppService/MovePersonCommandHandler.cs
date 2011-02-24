using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.Messages.Commands;
using NanoMessageBus;
using CommonDomain.Persistence;
using Sample.DomainModel;

namespace Sample.AppService
{
    public class MovePersonCommandHandler : IHandleMessages<MovePerson>
    {
        private readonly IRepository repository;

        public MovePersonCommandHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(MovePerson command)
        {
            Person person = repository.GetById<Person>(command.PersonId, int.MaxValue);
            person.MoveToAddress(new Address(command.Street, command.StreetNumber));
            repository.Save(person, Guid.NewGuid(), null);
        }
    }
}
