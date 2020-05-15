using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Business.Events
{
    public enum BusinessChangeType {         
        Add,
        Update,
        Delete
    }
    public class BusinessChangeEventArgs : BusinessEventArgs
    {        
        public BusinessChangeEventArgs(Type type) : base(type)
        {
        }

        public BusinessChangeType businessChangeType { get; set; }
        public BusinessObject entity { get; set; }
        public DateTime date_time = DateTime.Now;
    }
}
