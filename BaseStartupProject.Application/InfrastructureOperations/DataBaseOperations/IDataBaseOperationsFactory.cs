using BaseStartupProject.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.InfrastructureOperations.DataBaseOperations
{
    public interface IDataBaseOperationsFactory
    {
        ITableOperations CreateTableOperations(Type based_type, IRepositoryFactory repositoryFactory);        
    }
}
