using BaseStartupProject.Business.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Business
{
    public abstract class BusinessEntity : BusinessBase
    {
        protected bool _isIdSetted;

        protected BusinessEntity(IBusinessDataMediator handlers) : base(handlers)
        {

        }
        protected abstract void RaiseBusinessChange(BusinessChangeType type);
    }
}
