using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Mappers
{
    public interface IMapperFactory
    {
        IMapper<I, O> Create<I, O>();
    }
}
