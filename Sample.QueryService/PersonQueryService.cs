using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.ReadModel;
using StorageAccess;

namespace Sample.QueryService
{
    public class PersonQueryService : IPersonQueryService
    {
        private readonly IQueryStorage storage;

        public PersonQueryService(IQueryStorage storage)
        {
            this.storage = storage;
        }

        private IEnumerable<Person> FakeStore
        {
            get
            {
                yield return new Person { Id = Guid.NewGuid(), Name = "P1", Street = "S1", StreetNumber = "1" };
                yield return new Person { Id = Guid.NewGuid(), Name = "P2", Street = "S2", StreetNumber = "2" };
                yield return new Person { Id = Guid.NewGuid(), Name = "P3", Street = "S3", StreetNumber = "3" };
                yield return new Person { Id = Guid.NewGuid(), Name = "P4", Street = "S4", StreetNumber = "4" };
                yield return new Person { Id = Guid.NewGuid(), Name = "P5", Street = "S5", StreetNumber = "5" };
                yield return new Person { Id = Guid.NewGuid(), Name = "P6", Street = "S6", StreetNumber = "6" };
            }
        }

        public IQueryable<Person> QueryPersons
        {
            get
            {
                return storage.Items<Person>().AsQueryable();
            }
        }
    }
}
