using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Mappers
{
    public interface IMapperFactory
    {
        AbstractMapper<I, O> Create<I, O>();
    }
}
