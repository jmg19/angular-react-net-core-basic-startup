using IntimateDesires.Business.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Business
{
    public abstract class BusinessBase<T> : BusinessObject
    {               
        protected bool _isIdSetted;

        protected BusinessBase(IBusinessEvents handlers) : base(handlers)
        {
                    
        }
        protected abstract void RaiseBusinessChange(BusinessChangeType type);
        public abstract T Cast();
    }
}
