using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Commands.Funds
{
    public class CreateDocument
    {
        public readonly Guid Id;
        public readonly string AccessionNumber;     

        public CreateDocument(Guid id, string accessionNumber)
        {
            this.Id = id;
            this.AccessionNumber = accessionNumber;
        }          
    }
}
