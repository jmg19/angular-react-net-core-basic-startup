using BaseStartupProject.Application.AppModels.ResultModels;
using BaseStartupProject.Application.Queries.Users;
using BaseStartupProject.Infrastructure.Repositories.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Services.User
{
    public interface IUserQueriesService
    {
        UserAppModel GetUser(int id);
        IList<UserAppModel> GetAllUsers();
        IList<UserAppModel> GetUsers(UserQuery query);
        IList<UserAppModel> GetUsers(SearchRule[] search_rules, OrderingRule[] ordering_rules);
    }
}
