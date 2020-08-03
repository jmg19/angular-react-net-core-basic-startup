using BaseStartupProject.Application.AppModels.ResultModels;
using BaseStartupProject.Infrastructure.Repositories.DataTableObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Mappers
{
    public class MapperFactory : IMapperFactory
    {
        public IMapper<I, O> Create<I, O>()
        {
            string mapperClassName = string.Format("{0}.{1}To{2}Mapper", typeof(IMapper<I, O>).Namespace, typeof(I).Name, typeof(O).Name);
            string assemblyName = typeof(IMapper<I, O>).Assembly.FullName;
            
            var mapper = Activator.CreateInstance(assemblyName, mapperClassName).Unwrap();
            
            if (mapper != null) {
                return (IMapper<I, O>)mapper;
            }

            return null;
        }
    }
}
