using BaseStartupProject.Business.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application
{
    public class MediatorsFactory : IMediatorsFactory
    {
        public IBusinessDataMediator CreateBusinessDataMediator(EventHandler<BusinessConsultEventArgs> businessConsultHandler, EventHandler<BusinessChangeEventArgs> businessChangeHandler)
        {
            BusinessDataMediator mediator = new BusinessDataMediator();
            mediator.AppendBusinessConsultNeedHandler(businessConsultHandler);
            mediator.AppendBusinessChangeNeedHandler(businessChangeHandler);

            return mediator;
        }
    }
}
