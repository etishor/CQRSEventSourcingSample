using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages.Commands.Funds
{
    public class AssociateShareClassToDocument
    {
        public readonly Guid DocumentId;
        public readonly Guid ShareClassId;
        public readonly string ShareClassType;

        public AssociateShareClassToDocument(Guid documentId, Guid shareClassId, string shareClassType)
        {
            this.DocumentId = documentId;
            this.ShareClassId = shareClassId;
            this.ShareClassType = shareClassType;
        }
    }
}
