using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoMessageBus;
using Sample.Messages.Commands;
using CommonDomain.Persistence;
using Sample.DomainModel;

namespace Sample.AppService
{
    public class CreatePersonCommandHandler : IHandleMessages<CreatePerson>
    {
        private readonly IRepository repository;

        public CreatePersonCommandHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(CreatePerson command)
        {
            Console.WriteLine(command.Name);
            Person person = new Person(command.Id, new PersonName(command.Name), new Address(command.Street, command.StreetNumber));
            repository.Save(person, Guid.NewGuid(), null);
        }
    }
}
