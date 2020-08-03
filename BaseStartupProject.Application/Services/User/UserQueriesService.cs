using BaseStartupProject.Application.AppModels.ResultModels;
using BaseStartupProject.Application.Mappers;
using BaseStartupProject.Application.Queries.Users;
using BaseStartupProject.Infrastructure.Repositories;
using BaseStartupProject.Infrastructure.Repositories.DataTableObjects;
using BaseStartupProject.Infrastructure.Repositories.EntityFramework;
using BaseStartupProject.Infrastructure.Repositories.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Services.User
{
    public class UserQueriesService : BaseQueryService, IUserQueriesService
    {        
        public UserQueriesService(): base()
        {
            
        }

        public UserQueriesService(IRepositoryFactory repositoryFactory) : base(repositoryFactory)
        {
            
        }

        public IList<UserAppModel> GetAllUsers()
        {
            IReadOnlyRepository<DtoUser> readOnlyRepository = repositoryFactory.CreateReadOnlyUsersRepository();
            IEnumerable<DtoUser> users = readOnlyRepository.GetAll();
            IMapper<DtoUser, UserAppModel> mapper = mapperFactory.Create<DtoUser, UserAppModel>();

            return mapper.Map(users);
        }

        public UserAppModel GetUser(int id)
        {
            IReadOnlyRepository<DtoUser> readOnlyRepository = repositoryFactory.CreateReadOnlyUsersRepository();
            DtoUser user = readOnlyRepository.Get(id);
            IMapper<DtoUser, UserAppModel> mapper = mapperFactory.Create<DtoUser, UserAppModel>();

            return mapper.Map(user);
        }

        public IList<UserAppModel> GetUsers(UserQuery query)
        {
            IReadOnlyRepository<DtoUser> readOnlyRepository = repositoryFactory.CreateReadOnlyUsersRepository();
            IEnumerable<DtoUser> users = readOnlyRepository.Get(query.search_rules, query.ordering_rules);
            IMapper<DtoUser, UserAppModel> mapper = mapperFactory.Create<DtoUser, UserAppModel>();

            return mapper.Map(users);
        }

        public IList<UserAppModel> GetUsers(SearchRule[] search_rules, OrderingRule[] ordering_rules)
        {
            IReadOnlyRepository<DtoUser> readOnlyRepository = repositoryFactory.CreateReadOnlyUsersRepository();
            IEnumerable<DtoUser> users = readOnlyRepository.Get(search_rules, ordering_rules);
            IMapper<DtoUser, UserAppModel> mapper = mapperFactory.Create<DtoUser, UserAppModel>();

            return mapper.Map(users);
        }
    }
}
