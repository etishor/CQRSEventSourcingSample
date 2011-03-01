using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.ReadModel.Funds
{
    public class Document
    {
        public Guid Id { get; set; }
        public string AccessionNumber { get; set; }

        public ICollection<ShareClass> ShareClasses { get; set; }
    }
}
