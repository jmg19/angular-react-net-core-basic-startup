using IntimateDesires.Infrastructure.Repositories.DataTableObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Infrastructure.Repositories
{
    public interface IRepositoryFactory
    {
        IRepository<DtoUser> CreateUsersRepository();
        IReadOnlyRepository<DtoUser> CreateReadOnlyUsersRepository();

        void BeginChances();

        void SaveChances();
    }
}
