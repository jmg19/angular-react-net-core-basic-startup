using BaseStartupProject.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.InfrastructureOperations.DataBaseOperations
{
    public interface IDataBaseOperationsFactory
    {
        UsersTableOperations CreateUsersTableOperations(IRepositoryFactory repositoryFactory);
    }
}
