using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoMessageBus;
using Sample.Messages.Commands;
using CommonDomain.Persistence;
using Sample.DomainModel;
using Sample.DomainModel.People;
using Sample.Messages.Commands.People;

namespace Sample.AppService.People
{
    /// <summary>
    /// Command handler for the CreatePerson commant.
    /// Will construct a new Person Aggregate Root and store it in the repository.
    /// </summary>
    public class CreatePersonCommandHandler : IHandleMessages<CreatePerson>
    {
        private readonly IRepository repository;

        public CreatePersonCommandHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(CreatePerson command)
        {
            // create a new instance of the person aggregate using the name and address from the command
            Person person = Person.CreatePerson(command.Id, command.Name, command.Street, command.StreetNumber);

            repository.Save(person, Guid.NewGuid(), null);
        }
    }
}
