using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Infrastructure.Repositories.Utils
{
    public interface IFilterFactory
    {
        IList<ILinqFilter<T>> CreateFilters<T>(SearchRule[] rules);
    }

    public class FilterFactory : IFilterFactory
    {
        public IList<ILinqFilter<T>> CreateFilters<T>(SearchRule[] rules)
        {
            IList<ILinqFilter<T>> list = new List<ILinqFilter<T>>();

            foreach (SearchRule rule in rules) {
                LinqFilter<T> filter = new LinqFilter<T>() { searchRule = rule };
                list.Add(filter);
            }

            return list;
        }
    }
}
