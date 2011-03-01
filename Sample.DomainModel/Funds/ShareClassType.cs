using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.DomainModel.Funds
{
    public class ShareClassType
    {
        public ShareClassType(string type)
        {
            this.Type = type;
        }

        public string Type { get; private set; }
    }
}
