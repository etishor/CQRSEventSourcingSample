using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.DomainModel.Funds
{
    public class AccessionNumber
    {
        public AccessionNumber(string value)
        {
            this.Value = value;
        }

        public string Value { get; private set; }
    }
}
