using BaseStartupProject.Application.InfrastructureOperations.DataBaseOperations;
using BaseStartupProject.Business;
using BaseStartupProject.Business.Events;
using BaseStartupProject.Infrastructure.Repositories;
using BaseStartupProject.Infrastructure.Repositories.EntityFramework;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BaseStartupProject.Application.InfrastructureOperations
{
    public class InfrastructureOperationsFactory : IInfrastructureOperationsFactory
    {
        IRepositoryFactory repositoryFactory;
        IDataBaseOperationsFactory dataBaseOperationsFactory;

        public InfrastructureOperationsFactory()
        {
            repositoryFactory = new RepositoryFactory();
            dataBaseOperationsFactory = new DataBaseOperationsFactory();
        }

        public InfrastructureOperationsFactory(IRepositoryFactory repositoryFactory, IDataBaseOperationsFactory dataBaseOperationsFactory)
        {
            this.repositoryFactory = repositoryFactory;
            this.dataBaseOperationsFactory = dataBaseOperationsFactory;
        }

        private ITableOperations CreateTableOperation(BusinessEventArgs args)
        {
            ITableOperations operation = null;
            if (args.GetBusinessEntityType() == typeof(User))
            {
                operation = dataBaseOperationsFactory.CreateUsersTableOperations(repositoryFactory);
            }

            return operation;
        }

        public IInfrastructureOperation CreateDalChangeOperations(BusinessObject sender, BusinessChangeEventArgs args)
        {
            Task task = null;
            ITableOperations operation = CreateTableOperation(args);

            if (operation != null)
            {
                task = new Task(() =>
                {
                    switch (args.businessChangeType)
                    {
                        case BusinessChangeType.Add:
                            operation.Add(sender, args);
                            break;
                        case BusinessChangeType.Update:
                            operation.Update(sender, args);
                            break;
                        case BusinessChangeType.Delete:
                            operation.Delete(sender, args);
                            break;
                        default:
                            break;
                    }
                });
            }

            return new InfrastructureOperation(task);
        }

        public IInfrastructureOperation CreateDalConsultOperations(BusinessObject sender, BusinessConsultEventArgs args)
        {
            Task task = null;
            ITableOperations operation = CreateTableOperation(args);

            if (operation != null)
            {
                task = new Task(() =>
                {
                    switch (args.businessConsultType)
                    {
                        case BusinessConsultType.Get:
                            operation.Get(sender, args);
                            break;
                        case BusinessConsultType.GetAll:
                            operation.GetAll(sender, args);
                            break;
                        case BusinessConsultType.GetBy:
                            operation.GetBy(sender, args);
                            break;
                        default:
                            break;
                    }
                });
            }

            return new InfrastructureOperation(task);
        }

        public void SaveChanges()
        {
            repositoryFactory.SaveChances();
        }

        public class InfrastructureOperation : IInfrastructureOperation
        {
            Task task;
            public InfrastructureOperation(Task task)
            {
                this.task = task;
            }
            public void Execute()
            {
                if (task != null) 
                {
                    task.Start();
                    task.Wait();                
                }
            }
        }
    }
}
