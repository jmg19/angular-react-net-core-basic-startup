using IntimateDesires.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Application.InfrastructureOperationsDelegations.DataBaseOperations
{
    public class DataBaseOperationsFactory : IDataBaseOperationsFactory
    {
        public UsersTableOperations CreateUsersTableOperations(IRepositoryFactory repositoryFactory)
        {
            return new UsersTableOperations(repositoryFactory);
        }
    }
}
