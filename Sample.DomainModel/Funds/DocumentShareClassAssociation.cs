using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.DomainModel.Funds
{
    public class DocumentShareClassAssociation
    {
        public DocumentShareClassAssociation(Guid documentId, Guid shareClassId, ShareClassType shareClassType)
        {
            this.DocumentId = documentId;
            this.ShareClassId = shareClassId;
            this.ShareType = shareClassType;
        }

        public Guid DocumentId { get; private set; }
        public Guid ShareClassId { get; private set; }
        public ShareClassType ShareType { get; private set; }
    }
}
