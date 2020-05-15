using IntimateDesires.Business.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Business
{
    public abstract class BusinessObject
    {
        protected IBusinessEvents _handlers;

        public BusinessObject(IBusinessEvents handlers)
        {
            this._handlers = handlers;
        }

        public IBusinessEvents GetBusinessEvents()
        {
            return _handlers;
        }
    }
}
