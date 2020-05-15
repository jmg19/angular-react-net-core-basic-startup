using IntimateDesires.Business.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Business
{
    public abstract class BusinessBaseCollection<T> : BusinessObject
    {
        protected BusinessBaseCollection(IBusinessEvents handlers) : base (handlers)
        {            
        }

        internal T Get(int id) {
            BusinessConsultEventArgs args = new BusinessConsultEventArgs(typeof(T)) { entityId = id };
            _handlers.BusinessConsultNeed(this, args);

            if (args.entityResult != null)
            {
                return ((BusinessBase<T>)(args.entityResult)).Cast();
            }

            return default(T);
        }

        protected abstract void Add(T obj);                    
    }
}
