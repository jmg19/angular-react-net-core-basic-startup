using BaseStartupProject.Application.InfrastructureOperations;
using BaseStartupProject.Business;
using BaseStartupProject.Business.Events;

namespace BaseStartupProject.Application.Commands
{
    public abstract class AbstractComandHandler
    {
        protected IBusinessDataMediator businessDataMediator;                
        private IInfrastructureOperationsFactory infrastructureOperationsFactory;

        public AbstractComandHandler()
        {
            businessDataMediator = (new MediatorsFactory()).CreateBusinessDataMediator(handleBusinessNeed, handleBusinessNeed);
            infrastructureOperationsFactory = new InfrastructureOperationsFactory();
        }

        public AbstractComandHandler(IMediatorsFactory businessEventsFactory, IInfrastructureOperationsFactory infrastructureOperationsFactory)
        {
            businessDataMediator = businessEventsFactory.CreateBusinessDataMediator(handleBusinessNeed, handleBusinessNeed);
            this.infrastructureOperationsFactory = infrastructureOperationsFactory;
        }        

        protected void handleBusinessNeed(object sender, BusinessChangeEventArgs args)
        {
            IInfrastructureOperation operation = infrastructureOperationsFactory.CreateDalChangeOperations((BusinessBase)sender, args);
            operation.Execute();
        }

        protected void handleBusinessNeed(object sender, BusinessConsultEventArgs args)
        {
            IInfrastructureOperation operation = infrastructureOperationsFactory.CreateDalConsultOperations((BusinessBase)sender, args);
            operation.Execute();
        }
        protected void SaveChances()
        {
            infrastructureOperationsFactory.SaveChanges();
        }
    }
}