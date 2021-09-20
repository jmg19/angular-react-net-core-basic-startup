using BaseStartupProject.Business;
using BaseStartupProject.Business.Events;
using System.Collections.Generic;

namespace BaseStartupProject.Application.InfrastructureOperations.DataBaseOperations
{
    public interface ITableOperations
    {
        void Get(BusinessBase sender, BusinessConsultEventArgs args);
        void GetAll(BusinessBase sender, BusinessConsultEventArgs args);
        void GetBy(BusinessBase sender, BusinessConsultEventArgs args);
        void Add(BusinessBase sender, BusinessChangeEventArgs args);
        void Update(BusinessBase sender, BusinessChangeEventArgs args);
        void Delete(BusinessBase sender, BusinessChangeEventArgs args);
    }
}
