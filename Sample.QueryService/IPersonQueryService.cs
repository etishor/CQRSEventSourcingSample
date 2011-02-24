using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.ReadModel;

namespace Sample.QueryService
{
    public interface IPersonQueryService
    {
        IQueryable<Person> QueryPersons { get; }
    }
}
