using BaseStartupProject.Infrastructure.Repositories.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Infrastructure.Repositories
{
    public interface IRepository<T> : IReadOnlyRepository<T>
    {
        T Add(T dto);
        void Update(T dto);
        void Delete(int id);
        void SaveChances();
    }
}
