using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Messages
{
    public interface IEvent
    {
        /// <summary>
        /// The id of the aggregate root this event belongs to
        /// </summary>
        Guid AggregateId { get; }
    }
}
