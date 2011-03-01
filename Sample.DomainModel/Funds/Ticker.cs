using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.DomainModel.Funds
{
    public class Ticker
    {
        public Ticker(string symbol)
        {
            this.Symbol = symbol;
        }

        public string Symbol { get; private set; }
    }
}
