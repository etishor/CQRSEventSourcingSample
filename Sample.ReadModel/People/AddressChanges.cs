using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.ReadModel.People
{
    public class AddressChanges : ReadModelEntity
    {
        public AddressChanges(Guid id) : base(id) { }
                
        public string OldStreet { get; set; }
        public string OldNumber { get; set; }
        public string NewStreet { get; set; }
        public string NewNumber { get; set; }            
    }
}
