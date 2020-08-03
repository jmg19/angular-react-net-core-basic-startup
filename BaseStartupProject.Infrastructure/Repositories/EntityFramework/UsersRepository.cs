using BaseStartupProject.Infrastructure.Repositories.DataTableObjects;
using BaseStartupProject.Infrastructure.Repositories.RepositoryExceptions;
using BaseStartupProject.Infrastructure.Repositories.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseStartupProject.Infrastructure.Repositories.EntityFramework
{
    public class UsersRepository : AbstractRepositoryEF<DtoUser>
    {
        public UsersRepository(BaseStartupDemo context, bool readOnly) : base(context, readOnly)
        {
        }

        public UsersRepository(BaseStartupDemo context, bool readOnly, IFilterFactory filterFactory) : base(context, readOnly, filterFactory)
        {
        }

        public override DtoUser Add(DtoUser dto)
        {
            if (readOnly)
                throw new ReadOnlyException();

            context.Users.Add(dto);

            context.SaveChanges();

            return dto;
        }

        public override void Delete(int id)
        {
            if (readOnly)
                throw new ReadOnlyException();

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

        public override void Update(DtoUser dto)
        {
            if (readOnly)
                throw new ReadOnlyException();

            DtoUser current = (from user in context.Users where user.ID == dto.ID select user).FirstOrDefault();
            if (current != null)
            {
                current.UserName = dto.UserName;
                current.Hash = dto.Hash;
                current.Active = dto.Active;
            }
        }
    }
}
