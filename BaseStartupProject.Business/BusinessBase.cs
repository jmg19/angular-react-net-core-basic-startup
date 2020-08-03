using BaseStartupProject.Business.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Business
{
    public abstract class BusinessBase<T> : BusinessObject
    {               
        protected bool _isIdSetted;

        protected BusinessBase(IBusinessDataMediator handlers) : base(handlers)
        {
                    
        }
        protected abstract void RaiseBusinessChange(BusinessChangeType type);
        public abstract T Cast();
    }
}
