using BaseStartupProject.Business.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Business
{
    public abstract class BusinessBase
    {
        protected IBusinessDataMediator _mediator;

        public BusinessBase(IBusinessDataMediator mediator)
        {
            this._mediator = mediator;
        }

        public IBusinessDataMediator GetBusinessDataMediator()
        {
            return _mediator;
        }
    }
}
