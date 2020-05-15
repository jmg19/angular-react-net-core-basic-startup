using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Business.Events
{
    public class BusinessEventsFactory : IBusinessEventsFactory
    {
        public IBusinessEvents CreateBusinessEvents(EventHandler<BusinessConsultEventArgs> businessConsultHandler, EventHandler<BusinessChangeEventArgs> businessChangeHandler)
        {
            BusinessEvents events = new BusinessEvents();
            events.AppendBusinessConsultNeedHandler(businessConsultHandler);
            events.AppendBusinessChangeNeedHandler(businessChangeHandler);

            return events;
        }
    }
}
