using BaseStartupProject.Business;
using BaseStartupProject.Business.Events;
using System.Collections.Generic;

namespace BaseStartupProject.Application.InfrastructureOperations.DataBaseOperations
{
    public interface ITableOperations
    {
        void Get(BusinessObject sender, BusinessConsultEventArgs args);
        void GetAll(BusinessObject sender, BusinessConsultEventArgs args);
        void GetBy(BusinessObject sender, BusinessConsultEventArgs args);
        void Add(BusinessObject sender, BusinessChangeEventArgs args);
        void Update(BusinessObject sender, BusinessChangeEventArgs args);
        void Delete(BusinessObject sender, BusinessChangeEventArgs args);
    }
}
