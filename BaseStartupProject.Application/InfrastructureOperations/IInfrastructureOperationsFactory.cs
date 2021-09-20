using BaseStartupProject.Business;
using BaseStartupProject.Business.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.InfrastructureOperations
{
    public interface IInfrastructureOperationsFactory
    {
        IInfrastructureOperation CreateDalConsultOperations(BusinessBase sender, BusinessConsultEventArgs args);
        IInfrastructureOperation CreateDalChangeOperations(BusinessBase sender, BusinessChangeEventArgs args);
        void SaveChanges();
    }
}
