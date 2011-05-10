using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.ReadModel
{
    public abstract class ReadModelEntity
    {
        public string Id { get; private set; }

        public ReadModelEntity() { }// needed for asp.net model binder

        public ReadModelEntity(Guid id)
        {
            this.Id = MakeId(this.GetType(), id);
            this.AggregateId = id;
        }

        public Guid AggregateId { get; set; }
        
        private static string MakeId(Type type, Guid id)
        {
            return string.Concat(type.Name, "-", id);
        }             
    }
}
