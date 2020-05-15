using IntimateDesires.Infrastructure.Repositories.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntimateDesires.Infrastructure.Repositories
{
    public interface IReadOnlyRepository<T>
    {
        T Get(int id);
        IEnumerable<T> Get(SearchRule[] filters, OrderingRule[] ordering_rules);
        IEnumerable<T> GetAll();
        bool readOnly { get; }
    }
}
