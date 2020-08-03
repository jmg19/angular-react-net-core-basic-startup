using BaseStartupProject.Application.Mappers;
using BaseStartupProject.Infrastructure.Repositories;
using BaseStartupProject.Infrastructure.Repositories.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Services
{
    public abstract class BaseQueryService
    {
        protected IRepositoryFactory repositoryFactory;
        protected IMapperFactory mapperFactory;

        public BaseQueryService()
        {
            repositoryFactory = new RepositoryFactory();
            mapperFactory = new MapperFactory();
        }

        public BaseQueryService(IRepositoryFactory repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory;
            mapperFactory = new MapperFactory();
        }
    }
}
