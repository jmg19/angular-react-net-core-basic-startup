using IntimateDesires.Business;
using IntimateDesires.Business.Events;
using IntimateDesires.Infrastructure.Repositories;
using IntimateDesires.Infrastructure.Repositories.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Application.InfrastructureOperationsDelegations
{
    public class InfrastructureOperationDelegator : IInfrastructureOperationDelegator
    {
        IRepositoryFactory repositoryFactory;
        IInfrastructureOperationsFactory infrastructureOperationsFactory;

        public InfrastructureOperationDelegator()
        {
            repositoryFactory = new RepositoryFactory();
            infrastructureOperationsFactory = new InfrastructureOperationsFactory(repositoryFactory);
        }

        public InfrastructureOperationDelegator(IRepositoryFactory repositoryFactory, IInfrastructureOperationsFactory infrastructureOperationsFactory)
        {
            this.repositoryFactory = repositoryFactory;
            this.infrastructureOperationsFactory = infrastructureOperationsFactory;
        }

        public void handleBusinessNeed(object sender, BusinessChangeEventArgs args)
        {
            IInfrastructureOperation operation = infrastructureOperationsFactory.CreateDalChangeOperations((BusinessObject)sender, args);
            operation.Execute();
        }

        public void handleBusinessNeed(object sender, BusinessConsultEventArgs args)
        {
            IInfrastructureOperation operation = infrastructureOperationsFactory.CreateDalConsultOperations((BusinessObject)sender, args);
            operation.Execute();
        }

        public void SaveChances()
        {
            repositoryFactory.SaveChances();
        }
    }
}
