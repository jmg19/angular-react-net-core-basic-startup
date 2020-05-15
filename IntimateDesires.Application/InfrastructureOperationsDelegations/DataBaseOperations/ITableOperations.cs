using IntimateDesires.Business;
using IntimateDesires.Business.Events;
using System.Collections.Generic;

namespace IntimateDesires.Application.InfrastructureOperationsDelegations.DataBaseOperations
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
