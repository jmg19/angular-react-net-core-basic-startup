using BaseStartupProject.Infrastructure.Repositories.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Queries.Users
{
    public class UserByUsernameQuery : UserQuery
    {        
        public UserByUsernameQuery(string username) : base()
        {
            search_rules = new SearchRule[] { new SearchRule { condition = $"UserName == \"{username}\"" } };
            ordering_rules = new OrderingRule[] { };
        }
    }
}
