using IntimateDesires.Business;
using IntimateDesires.Business.Events;
using IntimateDesires.Infrastructure.Repositories;
using IntimateDesires.Infrastructure.Repositories.DataTableObjects;
using System.Collections.Generic;
using IntimateDesires.Infrastructure.Repositories.Utils;

namespace IntimateDesires.Application.InfrastructureOperationsDelegations.DataBaseOperations
{
    public class UsersTableOperations : BaseTableOperations<User>
    {
        public UsersTableOperations(IRepositoryFactory repositoryFactory) : base(repositoryFactory)
        {
        }

        public override void Add(BusinessObject sender, BusinessChangeEventArgs args)
        {
            User user = (User)(args.entity);
            DtoUser dtoUser = new DtoUser(user.id, user.username, user.hash, user.active);
            IRepository<DtoUser> repository = repositoryFactory.CreateUsersRepository();
            repository.Add(dtoUser);
        }

        public override void Delete(BusinessObject sender, BusinessChangeEventArgs args)
        {
            User user = (User)(args.entity);            
            IRepository<DtoUser> repository = repositoryFactory.CreateUsersRepository();
            repository.Delete(user.id);
        }

        public override void Get(BusinessObject sender, BusinessConsultEventArgs args)
        {                     
            IRepository<DtoUser> repository = repositoryFactory.CreateUsersRepository();
            DtoUser dtoUser =  repository.Get(args.entityId);
            User user = new User(dtoUser.ID, dtoUser.UserName, dtoUser.Hash, dtoUser.Active, sender.GetBusinessEvents());
            args.entityResult = user;
        }

        public override void GetAll(BusinessObject sender, BusinessConsultEventArgs args)
        {
            IRepository<DtoUser> repository = repositoryFactory.CreateUsersRepository();            
            args.entitiesListResult = new List<BusinessObject>();
            foreach (DtoUser dto in repository.GetAll()) {
                args.entitiesListResult.Add(new User(dto.ID, dto.UserName, dto.Hash, dto.Active, sender.GetBusinessEvents()));
            }
        }

        public override void GetBy(BusinessObject sender, BusinessConsultEventArgs args)
        {
            List<SearchRule> searchRules = new List<SearchRule>();
            foreach (string condition in args.conditions) 
            {
                searchRules.Add(new SearchRule { condition = condition });
            }

            IRepository<DtoUser> repository = repositoryFactory.CreateUsersRepository();
            args.entitiesListResult = new List<BusinessObject>();
            var getResult = repository.Get(searchRules.ToArray(), new OrderingRule[0]);
            foreach (DtoUser dto in getResult)
            {
                args.entitiesListResult.Add(new User(dto.ID, dto.UserName, dto.Hash, dto.Active, sender.GetBusinessEvents()));
            }
        }

        public override void Update(BusinessObject sender, BusinessChangeEventArgs args)
        {
            User user = (User)(args.entity);
            DtoUser dtoUser = new DtoUser(user.id, user.username, user.hash, user.active);
            IRepository<DtoUser> repository = repositoryFactory.CreateUsersRepository();
            repository.Update(dtoUser);
        }
    }
}
