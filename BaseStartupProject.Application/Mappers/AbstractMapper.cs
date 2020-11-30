using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Mappers
{
    public abstract class AbstractMapper<I, O>
    {
        public abstract O Map(I inObject);
        public IList<O> Map(IEnumerable<I> inObject)
        {
            List<O> list = new List<O>();
            foreach (I dto in inObject)
            {
                list.Add(Map(dto));
            }
            return list;
        }
    }
}
