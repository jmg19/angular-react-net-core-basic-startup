using BaseStartupProject.Business.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Business
{
    public abstract class BusinessObject
    {
        protected IBusinessDataMediator _mediator;

        public BusinessObject(IBusinessDataMediator mediator)
        {
            this._mediator = mediator;
        }

        public IBusinessDataMediator GetBusinessEvents()
        {
            return _mediator;
        }
    }
}
