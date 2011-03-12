using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Commands.Funds
{
    public class CreateShareClass
    {
        public CreateShareClass(Guid id, string ticker, string type)
        {
            this.Id = id;
            this.Ticker = ticker;
            this.Type = type;                
        }

        public Guid Id { get; set; }
        public string Ticker { get; set; }
        public string Type { get; set; }
    }
}
