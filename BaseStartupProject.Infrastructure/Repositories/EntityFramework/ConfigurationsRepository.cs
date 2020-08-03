using BaseStartupProject.Infrastructure.Repositories.DataTableObjects;
using BaseStartupProject.Infrastructure.Repositories.RepositoryExceptions;
using BaseStartupProject.Infrastructure.Repositories.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseStartupProject.Infrastructure.Repositories.EntityFramework
{
    public class ConfigurationsRepository : AbstractRepositoryEF<DtoConfiguration>
    {
        public ConfigurationsRepository(BaseStartupDemo context, bool readOnly) : base(context, readOnly)
        {
        }

        public ConfigurationsRepository(BaseStartupDemo context, bool readOnly, IFilterFactory filterFactory) : base(context, readOnly, filterFactory)
        {
        }

        public override DtoConfiguration Add(DtoConfiguration dto)
        {
            if (readOnly)
                throw new ReadOnlyException();

            context.Configurations.Add(new DtoConfiguration() { Name = dto.Name, Value = dto.Value });
            return context.Configurations.Last();
        }

        public override void Delete(int id)
        {
            if (readOnly)
                throw new ReadOnlyException();

            DtoConfiguration current = (from config in context.Configurations where config.ID == id select config).FirstOrDefault();
            if (current != null)
            {
                context.Configurations.Remove(current);
            }
        }

        public override DtoConfiguration Get(int id)
        {
            DtoConfiguration current = (from config in context.Configurations where config.ID == id select config).FirstOrDefault();
            return current;
        }

        public override IEnumerable<DtoConfiguration> Get(SearchRule[] filters, OrderingRule[] ordering_rules)
        {
            return new DtoConfiguration[] { };
        }

        public override IEnumerable<DtoConfiguration> GetAll()
        {
            return context.Configurations;
        }

        public override void Update(DtoConfiguration dto)
        {
            if (readOnly)
                throw new ReadOnlyException();

            DtoConfiguration current = (from config in context.Configurations where config.ID == dto.ID select config).FirstOrDefault();
            if (current != null)
            {
                current.Name = dto.Name;
                current.Value = dto.Value;
            }
        }
    }
}
