using BaseStartupProject.Infrastructure.Repositories.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace BaseStartupProject.Infrastructure.Repositories.EntityFramework
{
    public abstract class AbstractRepositoryEF<T>: IRepository<T>, IReadOnlyRepository<T>
    {
        protected BaseStartupDemo context;
        protected IFilterFactory filterFactory;
        public bool readOnly { get; private set; }

        public AbstractRepositoryEF(BaseStartupDemo context, bool readOnly)
        {
            this.context = context;
            this.readOnly = readOnly;
            filterFactory = new FilterFactory();
        }

        public AbstractRepositoryEF(BaseStartupDemo context, bool readOnly, IFilterFactory filterFactory)
        {
            this.context = context;
            this.readOnly = readOnly;
            this.filterFactory = filterFactory;
        }

        public abstract T Add(T dto);
        public abstract void Update(T dto);
        public abstract void Delete(int id);
        public abstract IEnumerable<T> GetAll();
        
        public abstract T Get(int id);

        public abstract IEnumerable<T> Get(SearchRule[] filters, OrderingRule[] ordering_rules);

        public virtual void SaveChances()
        {
            context.SaveChanges();
        }
    }
}
