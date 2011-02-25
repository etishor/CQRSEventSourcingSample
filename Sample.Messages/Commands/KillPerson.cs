using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Commands
{
    public class KillPerson
    {
        public KillPerson(Guid id)
        {
            this.VictimId = id;
        }

        public Guid VictimId { get; set; }
    }
}
