using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Business.Events
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
        public IEnumerable<BusinessObject> result { get; set; }
    }
}
