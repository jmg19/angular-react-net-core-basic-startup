using IntimateDesires.Business.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Application.InfrastructureOperationsDelegations
{
    public interface IInfrastructureOperationDelegator
    {
        void handleBusinessNeed(object sender, BusinessChangeEventArgs args);
        void handleBusinessNeed(object sender, BusinessConsultEventArgs args);
        void SaveChances();
    }
}
