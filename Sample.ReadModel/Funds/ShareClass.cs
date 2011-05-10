using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.ReadModel.Funds
{
    public class ShareClass : ReadModelEntity
    {
        public ShareClass() { } // needed for asp.net model binder

        public ShareClass(Guid id) : base(id) { }

        public string Ticker { get; set; }
        public string Type { get; set; }
    }
}
