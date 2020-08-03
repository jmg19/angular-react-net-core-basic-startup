using BaseStartupProject.Business.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseStartupProject.Business
{
    public abstract class BusinessBaseCollection<T> : BusinessObject
    {
        protected BusinessBaseCollection(IBusinessDataMediator handlers) : base (handlers)
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

        protected abstract void Add(T obj);                    
    }
}
