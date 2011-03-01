using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.Messages;
using CommonDomain.Core;
using Sample.Messages.Events.Funds;

namespace Sample.DomainModel.Funds
{
    public class Document : AggregateBase<IEvent>
    {
        private AccessionNumber accessionNumber;
        private IList<DocumentShareClassAssociation> shareClassAssociations = new List<DocumentShareClassAssociation>();

        private Document(Guid id)
        {
            this.Id = id;
        }

        public Document(Guid id, AccessionNumber accessionNumber)
            :this(id)
        {
            RaiseEvent(new DocumentCreated(id, accessionNumber.Value));
        }

        public void AssociateWithShareClass(DocumentShareClassAssociation association)
        {
            RaiseEvent(new DocumentAssociatedWithShareclass(this.Id, association.ShareClassId, association.ShareType.Type));
        }

        private void Apply(DocumentCreated @event)
        {
            this.accessionNumber = new AccessionNumber(@event.AccessionNumber);
        }

        private void Apply(DocumentAssociatedWithShareclass @event)
        {
            this.shareClassAssociations.Add(new DocumentShareClassAssociation(@event.AggregateId, @event.ShareClassId, new ShareClassType(@event.ShareClassType)));
        }
    }
}
