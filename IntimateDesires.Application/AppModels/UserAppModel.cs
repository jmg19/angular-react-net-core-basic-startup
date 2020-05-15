using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Application.AppModels.ResultModels
{
    public class UserAppModel
    {        
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Hash { get; set; }
        public bool Active { get; set; }
    }
}
