using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Mappers
{
    public interface IMapper<I, O>
    {
        O Map(I inObject);
        IList<O> Map(IEnumerable<I> inObject);
    }
}
