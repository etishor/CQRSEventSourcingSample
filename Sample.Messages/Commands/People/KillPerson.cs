using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Commands.People
{
    public sealed class KillPerson
    {
        public readonly Guid VictimId;

        public KillPerson(Guid victimId)
        {
            this.VictimId = victimId;
        }
    }
}
