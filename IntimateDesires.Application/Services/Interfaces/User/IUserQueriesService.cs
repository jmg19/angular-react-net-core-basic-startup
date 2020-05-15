using IntimateDesires.Application.AppModels.ResultModels;
using IntimateDesires.Infrastructure.Repositories.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Application.Services.Interfaces.User
{
    public interface IUserQueriesService
    {
        UserAppModel GetUser(int id);
        IList<UserAppModel> GetAllUsers();

        IList<UserAppModel> GetUsers(SearchRule[] search_rules, OrderingRule[] ordering_rules);
    }
}
