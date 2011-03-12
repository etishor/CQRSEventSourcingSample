using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Commands.Funds
{
    public class AssociateShareClassToDocument
    {
        public AssociateShareClassToDocument(Guid documentId, Guid shareClassId, string shareType)
        {
            this.DocumentId = documentId;
            this.ShareClassId = shareClassId;
            this.ShareClassType = shareType;
        }
                
        public Guid DocumentId { get; set; }
        public Guid ShareClassId { get; set; }
        public string ShareClassType { get; set; }
    }
}
