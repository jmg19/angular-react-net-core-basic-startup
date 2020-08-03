using BaseStartupProject.Infrastructure.Repositories.DataTableObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Infrastructure.Repositories
{
    public interface IRepositoryFactory
    {
        IRepository<DtoUser> CreateUsersRepository();
        IReadOnlyRepository<DtoUser> CreateReadOnlyUsersRepository();

        IRepository<DtoConfiguration> CreateConfigurationsRepository();
        IReadOnlyRepository<DtoConfiguration> CreateReadOnlyConfigurationsRepository();

        void BeginChances();

        void SaveChances();
    }
}
