using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Business.Events
{
    public interface IBusinessEventsFactory
    {
        IBusinessEvents CreateBusinessEvents(EventHandler<BusinessConsultEventArgs> businessConsultHandler, EventHandler<BusinessChangeEventArgs> businessChangeHandler);
    }
}
