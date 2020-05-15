using IntimateDesires.Infrastructure.Repositories.DataTableObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Infrastructure.Repositories.EntityFramework
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private IInfrastructureConfigurationLoader configuration_loader = new InfrastructureConfigurationLoader();
        private IntimateDesiresContext _context;

        public RepositoryFactory()
        {
            InfrastructureConfigurations configs = configuration_loader.Load("appsettings.json");
            var optionsBuilder = new DbContextOptionsBuilder<IntimateDesiresContext>();
            optionsBuilder.UseSqlServer(configs.sql_Connection);

            _context = new IntimateDesiresContext(optionsBuilder.Options);
        }

        public RepositoryFactory(IInfrastructureConfigurationLoader configuration_loader)
        {
            InfrastructureConfigurations configs = configuration_loader.Load("appsettings.json");
            var optionsBuilder = new DbContextOptionsBuilder<IntimateDesiresContext>();
            optionsBuilder.UseSqlServer(configs.sql_Connection);

            _context = new IntimateDesiresContext(optionsBuilder.Options);
        }

        public IRepository<DtoUser> CreateUsersRepository()
        {
            return new UserRepository(_context, false);
        }

        public IReadOnlyRepository<DtoUser> CreateReadOnlyUsersRepository()
        {
            return new UserRepository(_context, true);
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
