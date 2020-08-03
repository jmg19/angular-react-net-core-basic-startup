using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Infrastructure.Repositories.Utils
{    
    public struct PagingRule {
        public int page;
        public int page_size;
    }

    public class SearchRule
    {                
        public string condition { get; set; }
        public PagingRule paging_rule { get; set; }
    }
}
