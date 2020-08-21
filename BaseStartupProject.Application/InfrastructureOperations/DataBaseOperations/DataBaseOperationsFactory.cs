using BaseStartupProject.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.InfrastructureOperations.DataBaseOperations
{
    public class DataBaseOperationsFactory : IDataBaseOperationsFactory
    {
        public ITableOperations CreateTableOperations(Type based_type, IRepositoryFactory repositoryFactory)
        {
            Type type = Type.GetType(string.Format("{0}.{1}TableOperations", GetType().Namespace, based_type.Name));
            return (ITableOperations)(Activator.CreateInstance(type, repositoryFactory));
        }

        public UserTableOperations CreateUsersTableOperations(IRepositoryFactory repositoryFactory)
        {
            return new UserTableOperations(repositoryFactory);
        }
    }
}
