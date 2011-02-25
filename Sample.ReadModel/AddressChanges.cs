using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.ReadModel
{
    public class AddressChanges
    {
        // for nhibernate
        protected Guid Id { get; set; }
        public string OldStreet { get; set; }
        public string OldNumber { get; set; }
        public string NewStreet { get; set; }
        public string NewNumber { get; set; }            
    }
}
