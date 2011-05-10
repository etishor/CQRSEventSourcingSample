using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Commands.Funds
{
    public sealed class CreateShareClass
    {
        public readonly Guid Id;
        public readonly string Ticker;
        public readonly string Type;

        public CreateShareClass(Guid id, string ticker, string type)
        {
            this.Id = id;
            this.Ticker = ticker;
            this.Type = type;                
        }       
    }
}
