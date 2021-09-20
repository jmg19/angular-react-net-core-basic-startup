using BaseStartupProject.Business.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseStartupProject.Business
{
    public abstract class BusinessAggregate<T> : BusinessBase
    {
        protected BusinessAggregate(IBusinessDataMediator handlers) : base (handlers)
        {            
        }

        public T Get(int id) {            
            T user = _mediator.MediateBusinessDataConsult<T>(this, id).FirstOrDefault();

            if (user != null)
            {
                return user;
            }

            return default(T);
        }

        protected IEnumerable<T> GetBy(string[] conditions)
        {
            return _mediator.MediateBusinessDataConsult<T>(this, conditions);
        }

        protected internal virtual void Add(BusinessEntity obj)
        {
            _mediator.MediateBusinessDataChange<T>(this, BusinessChangeType.Add, obj);
        }

        protected internal virtual void Delete(BusinessEntity obj)
        {
            _mediator.MediateBusinessDataChange<T>(this, BusinessChangeType.Delete, obj);
        }
        public abstract void Delete(int id);
    }
}
