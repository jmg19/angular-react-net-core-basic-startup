using IntimateDesires.Business;
using IntimateDesires.Business.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Application.InfrastructureOperationsDelegations
{
    public interface IInfrastructureOperationsFactory
    {
        IInfrastructureOperation CreateDalConsultOperations(BusinessObject sender, BusinessConsultEventArgs args);
        IInfrastructureOperation CreateDalChangeOperations(BusinessObject sender, BusinessChangeEventArgs args);
    }
}
