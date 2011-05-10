using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.ReadModel.Funds
{
    public class Document : ReadModelEntity
    {
        public Document() { }// needed for asp.net model binder
        public Document(Guid id) : base(id) { }

        public string AccessionNumber { get; set; }
        public ICollection<ShareClass> ShareClasses { get; set; }
    }
}
