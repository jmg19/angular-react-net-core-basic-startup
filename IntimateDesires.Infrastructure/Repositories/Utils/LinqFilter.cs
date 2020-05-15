using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

namespace IntimateDesires.Infrastructure.Repositories.Utils
{
    public interface ILinqFilter<T> {
        IQueryable<T> Filter(IQueryable<T> source);
    }

    public class LinqFilter<T> : ILinqFilter<T>
    {
        public SearchRule searchRule { get; set; }

        public IQueryable<T> Filter(IQueryable<T> source)
        {
            if (searchRule.paging_rule.page_size == 0) 
            {
                return source.Where(searchRule.condition);
            }
            else
            {
                if (searchRule.paging_rule.page <= 0)
                {
                    return source.Take(searchRule.paging_rule.page_size);
                }
                else 
                {                 
                    return source.Skip(searchRule.paging_rule.page_size * (searchRule.paging_rule.page - 1)).Take(searchRule.paging_rule.page_size);
                }
            }
        }
    }
}
