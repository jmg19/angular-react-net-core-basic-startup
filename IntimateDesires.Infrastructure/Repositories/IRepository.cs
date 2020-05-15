using IntimateDesires.Infrastructure.Repositories.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Infrastructure.Repositories
{
    public interface IRepository<T> : IReadOnlyRepository<T>
    {
        T Add(T dto);
        void Update(T dto);
        void Delete(int id);
        void SaveChances();
    }
}
