using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonDomain.Persistence;
using CommonDomain;
using System.Reflection;

namespace Sample.AppService
{
    /// <summary>
    /// Factory for creating aggregates from with their Id using a private constructor that accespts
    /// only one paramenter, the id of the aggregate to create.
    /// This factory is used by the event store to create an aggregate prior to replaying it's events.
    /// </summary>
    public class AggregateFactory : IConstructAggregates
    {
        public IAggregate Build(Type type, Guid id, IMemento snapshot)
        {
            ConstructorInfo constructor = type.GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(Guid) }, null);

            return constructor.Invoke(new object[] { id }) as IAggregate;
        }
    }
}
