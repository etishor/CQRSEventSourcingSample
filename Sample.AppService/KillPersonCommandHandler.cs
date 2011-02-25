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
    public class KillPersonCommandHandler : IHandleMessages<KillPerson>
    {
        private readonly IRepository repository;

        public KillPersonCommandHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(KillPerson command)
        {
            Person person = repository.GetById<Person>(command.VictimId, int.MaxValue);
            
            person.Kill();

            repository.Save(person, Guid.NewGuid(), null);
        }
    }
}
