using BaseStartupProject.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.InfrastructureOperations.DataBaseOperations
{
    public class DataBaseOperationsFactory : IDataBaseOperationsFactory
    {
        public UsersTableOperations CreateUsersTableOperations(IRepositoryFactory repositoryFactory)
        {
            return new UsersTableOperations(repositoryFactory);
        }
    }
}
