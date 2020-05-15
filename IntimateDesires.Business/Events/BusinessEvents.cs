using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Business.Events
{
    public class BusinessEvents : IBusinessEvents
    {
        public EventHandler<BusinessConsultEventArgs> BusinessConsultNeed { get { return _BusinessConsultNeed; } private set { } }
        public EventHandler<BusinessChangeEventArgs> BusinessChangeNeed { get { return _BusinessChangeNeed; } private set { } }

        private event EventHandler<BusinessConsultEventArgs> _BusinessConsultNeed;
        private event EventHandler<BusinessChangeEventArgs> _BusinessChangeNeed;

        public void AppendBusinessConsultNeedHandler(EventHandler<BusinessConsultEventArgs> handler)
        {
            _BusinessConsultNeed += handler;
        }

        public void AppendBusinessChangeNeedHandler(EventHandler<BusinessChangeEventArgs> handler)
        {
            _BusinessChangeNeed += handler;
        }
    }
}
