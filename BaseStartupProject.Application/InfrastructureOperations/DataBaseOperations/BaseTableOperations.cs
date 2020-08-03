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
        public abstract void Get(BusinessObject sender, BusinessConsultEventArgs args);
        public abstract void GetAll(BusinessObject sender, BusinessConsultEventArgs args);
        public abstract void GetBy(BusinessObject sender, BusinessConsultEventArgs args);
        public abstract void Add(BusinessObject sender, BusinessChangeEventArgs args);
        public abstract void Update(BusinessObject sender, BusinessChangeEventArgs args);
        public abstract void Delete(BusinessObject sender, BusinessChangeEventArgs args);
        
    }
}
