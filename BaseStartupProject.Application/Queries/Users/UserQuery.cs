using BaseStartupProject.Application.Commands;
using BaseStartupProject.Infrastructure.Repositories.DataTableObjects;
using BaseStartupProject.Infrastructure.Repositories.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Queries.Users
{
    public class UserQuery : IQuery
    {
        public UserQuery()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public SearchRule[] search_rules { get; set; } 
        public OrderingRule[] ordering_rules { get; set; }        
    }
}
