using IntimateDesires.Infrastructure.Repositories.DataTableObjects;
using IntimateDesires.Infrastructure.Repositories.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntimateDesires.Infrastructure.Repositories.EntityFramework
{
    public class UserRepository : AbstractRepositoryEF<DtoUser>
    {
        public UserRepository(IntimateDesiresContext context, bool readOnly) : base(context, readOnly)
        {
        }

        public UserRepository(IntimateDesiresContext context, bool readOnly, IFilterFactory filterFactory) : base(context, readOnly, filterFactory)
        {
        }

        public override DtoUser Add(DtoUser dto)
        {
            context.Users.Add(new DtoUser() { UserName = dto.UserName, Hash = dto.Hash, Active = dto.Active });
            return context.Users.Last();
        }

        public override void Update(DtoUser dto)
        {
            DtoUser current = (from user in context.Users where user.ID == dto.ID select user).FirstOrDefault();
            if (current != null) {
                current.UserName = dto.UserName;
                current.Hash = dto.Hash;
                current.Active = dto.Active;
            }
        }

        public override void Delete(int id)
        {
            DtoUser current = (from user in context.Users where user.ID == id select user).FirstOrDefault();
            if (current != null)
            {
                context.Users.Remove(current);
            }
        }

        public override DtoUser Get(int id)
        {
            DtoUser current = (from user in context.Users where user.ID == id select user).FirstOrDefault();
            return current;
        }

        public override IEnumerable<DtoUser> Get(SearchRule[] search_rules, OrderingRule[] ordering_rules)
        {

            IList<ILinqFilter<DtoUser>> filters = filterFactory.CreateFilters<DtoUser>(search_rules);
            var result = context.Users.OrderByDynamic(ordering_rules);
            foreach(ILinqFilter<DtoUser> f in filters)
            {
                result = f.Filter(result);
            }

            return result;
        }

        public override IEnumerable<DtoUser> GetAll()
        {
            return context.Users;
        }
    }
}
