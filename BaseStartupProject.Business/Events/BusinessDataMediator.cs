using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Business.Events
{
    public class BusinessDataMediator : IBusinessDataMediator
    {
        private event EventHandler<BusinessConsultEventArgs> BusinessConsultNeed;
        private event EventHandler<BusinessChangeEventArgs> BusinessChangeNeed;

        public void AppendBusinessConsultNeedHandler(EventHandler<BusinessConsultEventArgs> handler)
        {
            BusinessConsultNeed += handler;
        }

        public void AppendBusinessChangeNeedHandler(EventHandler<BusinessChangeEventArgs> handler)
        {
            BusinessChangeNeed += handler;
        }

        public IEnumerable<T> MediateBusinessDataConsult<T>(object sender)
        {
            BusinessConsultEventArgs args = new BusinessConsultEventArgs(typeof(T)) { businessConsultType = BusinessConsultType.GetAll };
            BusinessConsultNeed(sender, args);
            return (IEnumerable<T>)args.result;
        }

        public IEnumerable<T> MediateBusinessDataConsult<T>(object sender, int entityId)
        {
            BusinessConsultEventArgs args = new BusinessConsultEventArgs(typeof(T)) { businessConsultType = BusinessConsultType.Get, entityId = entityId };
            BusinessConsultNeed(sender, args);
            return (IEnumerable<T>)args.result;
        }

        public IEnumerable<T> MediateBusinessDataConsult<T>(object sender, string[] conditions)
        {
            BusinessConsultEventArgs args = new BusinessConsultEventArgs(typeof(T)) { businessConsultType = BusinessConsultType.GetBy, conditions = conditions };
            BusinessConsultNeed(sender, args);
            return (IEnumerable<T>)args.result;
        }

        public void MediateBusinessDataChange<T>(object sender, BusinessChangeType type, BusinessObject entity)
        {
            BusinessChangeEventArgs args = new BusinessChangeEventArgs(typeof(T)) { businessChangeType = type, entity = entity };
            BusinessChangeNeed(sender, args);
        }
    }
}
