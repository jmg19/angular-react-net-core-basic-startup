using BaseStartupProject.Business;
using BaseStartupProject.Business.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.InfrastructureOperations
{
    public interface IInfrastructureOperationsFactory
    {
        IInfrastructureOperation CreateDalConsultOperations(BusinessObject sender, BusinessConsultEventArgs args);
        IInfrastructureOperation CreateDalChangeOperations(BusinessObject sender, BusinessChangeEventArgs args);
        void SaveChanges();
    }
}
