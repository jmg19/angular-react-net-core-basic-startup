using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Business.Events
{
    public enum BusinessConsultType { 
        Get,
        GetAll,
        GetBy
    }
    public class BusinessConsultEventArgs : BusinessEventArgs
    {
        public BusinessConsultEventArgs(Type type) : base(type)
        {
        }

        public BusinessConsultType businessConsultType { get; set; }
        public int entityId { get; set; }
        public string[] conditions { get; set; }
        public BusinessObject entityResult { get; set; }
        public List<BusinessObject> entitiesListResult { get; set; }
    }
}
