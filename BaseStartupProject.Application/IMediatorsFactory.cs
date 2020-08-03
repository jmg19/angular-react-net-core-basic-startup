using BaseStartupProject.Business.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application
{
    public interface IMediatorsFactory
    {
        IBusinessDataMediator CreateBusinessDataMediator(EventHandler<BusinessConsultEventArgs> businessConsultHandler, EventHandler<BusinessChangeEventArgs> businessChangeHandler);
    }
}
