using System;

namespace IntimateDesires.Business.Events
{
    public interface IBusinessEvents
    {
        EventHandler<BusinessConsultEventArgs> BusinessConsultNeed { get; }
        EventHandler<BusinessChangeEventArgs> BusinessChangeNeed { get; }
        void AppendBusinessConsultNeedHandler(EventHandler<BusinessConsultEventArgs> handler);
        void AppendBusinessChangeNeedHandler(EventHandler<BusinessChangeEventArgs> handler);        
    }
}