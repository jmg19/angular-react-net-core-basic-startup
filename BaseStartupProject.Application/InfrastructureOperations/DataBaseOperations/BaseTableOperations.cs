using BaseStartupProject.Business;
using BaseStartupProject.Business.Events;
using BaseStartupProject.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.InfrastructureOperations.DataBaseOperations
{
    public abstract class BaseTableOperations<T> : ITableOperations
    {
        protected IRepositoryFactory repositoryFactory;

        public BaseTableOperations(IRepositoryFactory repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory;
        }
        public abstract void Get(BusinessBase sender, BusinessConsultEventArgs args);
        public abstract void GetAll(BusinessBase sender, BusinessConsultEventArgs args);
        public abstract void GetBy(BusinessBase sender, BusinessConsultEventArgs args);
        public abstract void Add(BusinessBase sender, BusinessChangeEventArgs args);
        public abstract void Update(BusinessBase sender, BusinessChangeEventArgs args);
        public abstract void Delete(BusinessBase sender, BusinessChangeEventArgs args);
        
    }
}
