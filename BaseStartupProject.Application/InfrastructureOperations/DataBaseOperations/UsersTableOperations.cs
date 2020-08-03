using BaseStartupProject.Business;
using BaseStartupProject.Business.Events;
using BaseStartupProject.Infrastructure.Repositories;
using BaseStartupProject.Infrastructure.Repositories.DataTableObjects;
using System.Collections.Generic;
using BaseStartupProject.Infrastructure.Repositories.Utils;

namespace BaseStartupProject.Application.InfrastructureOperations.DataBaseOperations
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
            dtoUser = repository.Add(dtoUser);
            user.id = dtoUser.ID;
        }

        public override void Delete(BusinessObject sender, BusinessChangeEventArgs args)
        {
            User user = (User)(args.entity);            
            IRepository<DtoUser> repository = repositoryFactory.CreateUsersRepository();
            repository.Delete(user.id);
        }

        public override void Get(BusinessObject sender, BusinessConsultEventArgs args)
        {                     
            IReadOnlyRepository<DtoUser> repository = repositoryFactory.CreateReadOnlyUsersRepository();
            DtoUser dtoUser =  repository.Get(args.entityId);
            User user = new User(dtoUser.ID, dtoUser.UserName, dtoUser.Hash, dtoUser.Active, sender.GetBusinessEvents());
            args.result = new List<User>() { user };
        }

        public override void GetAll(BusinessObject sender, BusinessConsultEventArgs args)
        {
            IReadOnlyRepository<DtoUser> repository = repositoryFactory.CreateReadOnlyUsersRepository();
            List<User> list = new List<User>();
            foreach (DtoUser dto in repository.GetAll()) {
                list.Add(new User(dto.ID, dto.UserName, dto.Hash, dto.Active, sender.GetBusinessEvents()));
            }
            args.result = list;
        }

        public override void GetBy(BusinessObject sender, BusinessConsultEventArgs args)
        {
            List<SearchRule> searchRules = new List<SearchRule>();
            foreach (string condition in args.conditions) 
            {
                searchRules.Add(new SearchRule { condition = condition });
            }

            IReadOnlyRepository<DtoUser> repository = repositoryFactory.CreateReadOnlyUsersRepository();
            List<User> list = new List<User>();
            var getResult = repository.Get(searchRules.ToArray(), new OrderingRule[0]);
            foreach (DtoUser dto in getResult)
            {
                list.Add(new User(dto.ID, dto.UserName, dto.Hash, dto.Active, sender.GetBusinessEvents()));
            }
            args.result = list;
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
