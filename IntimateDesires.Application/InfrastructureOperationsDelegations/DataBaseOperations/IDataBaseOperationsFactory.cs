using IntimateDesires.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Application.InfrastructureOperationsDelegations.DataBaseOperations
{
    public interface IDataBaseOperationsFactory
    {
        UsersTableOperations CreateUsersTableOperations(IRepositoryFactory repositoryFactory);
    }
}
