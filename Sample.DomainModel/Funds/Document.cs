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
            if (!association.ShareType.IsLinkable)
            {
                throw new InvalidOperationException("Only linkable share classes can be associated with documents");
            }

            RaiseEvent(new DocumentAssociatedWithShareclass(this.Id, association.ShareClassId, association.ShareType.Name.ToString()));
        }

        private void Apply(DocumentCreated @event)
        {
            this.accessionNumber = new AccessionNumber(@event.AccessionNumber);
        }

        private void Apply(DocumentAssociatedWithShareclass @event)
        {
            this.shareClassAssociations.Add(
                new DocumentShareClassAssociation(@event.AggregateId, @event.ShareClassId,
                    ShareClassType.CreateFromString(@event.ShareClassType)));
        }
    }
}
