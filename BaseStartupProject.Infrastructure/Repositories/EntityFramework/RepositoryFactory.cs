using BaseStartupProject.Infrastructure.Repositories.DataTableObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Infrastructure.Repositories.EntityFramework
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private IInfrastructureConfigurationLoader configuration_loader = new InfrastructureConfigurationLoader();
        private BaseStartupDemo _context;

        public RepositoryFactory()
        {
            InfrastructureConfigurations configs = configuration_loader.Load("appsettings.json");
            var optionsBuilder = new DbContextOptionsBuilder<BaseStartupDemo>();
            if(configs.sql_Connection != null)
            {
                optionsBuilder.UseSqlServer(configs.sql_Connection);
            }

            _context = new BaseStartupDemo(optionsBuilder.Options);
        }

        public RepositoryFactory(IInfrastructureConfigurationLoader configuration_loader)
        {
            InfrastructureConfigurations configs = configuration_loader.Load("appsettings.json");
            var optionsBuilder = new DbContextOptionsBuilder<BaseStartupDemo>();
            optionsBuilder.UseSqlServer(configs.sql_Connection);

            _context = new BaseStartupDemo(optionsBuilder.Options);
        }

        public IRepository<DtoUser> CreateUsersRepository()
        {
            return new UsersRepository(_context, false);
        }

        public IReadOnlyRepository<DtoUser> CreateReadOnlyUsersRepository()
        {
            return new UsersRepository(_context, true);
        }

        public IRepository<DtoConfiguration> CreateConfigurationsRepository()
        {
            return new ConfigurationsRepository(_context, false);
        }

        public IReadOnlyRepository<DtoConfiguration> CreateReadOnlyConfigurationsRepository()
        {
            return new ConfigurationsRepository(_context, true);
        }

        public void BeginChances()
        {
            
        }

        public void SaveChances()
        {
            _context.SaveChanges();
        }
    }
}
