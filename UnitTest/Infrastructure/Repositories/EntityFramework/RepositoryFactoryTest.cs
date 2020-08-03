using BaseStartupProject.Infrastructure;
using BaseStartupProject.Infrastructure.Repositories;
using BaseStartupProject.Infrastructure.Repositories.DataTableObjects;
using BaseStartupProject.Infrastructure.Repositories.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Tests.Infrastructure.Repositories.EntityFramework
{
    [TestClass]
    public class RepositoryFactoryTest
    {
        [TestMethod]
        public void CreateUsersRepository_Test() {
            Mock<IInfrastructureConfigurationLoader> mockConfigLoader = new Mock<IInfrastructureConfigurationLoader>();
            mockConfigLoader.Setup(x => x.Load(It.IsAny<string>())).Returns(new InfrastructureConfigurations
            {
                sql_Connection = "Mocked Connection"
            });

            RepositoryFactory factory = new RepositoryFactory(mockConfigLoader.Object);

            IRepository<DtoUser> userRepo = factory.CreateUsersRepository();
            IReadOnlyRepository<DtoUser> userRepoReadOnly = factory.CreateReadOnlyUsersRepository();

            Assert.IsInstanceOfType(userRepo, typeof(UsersRepository));
            Assert.IsInstanceOfType(userRepoReadOnly, typeof(UsersRepository));
        }

        [TestMethod]
        public void CreateConfigurationsRepository_Test()
        {
            Mock<IInfrastructureConfigurationLoader> mockConfigLoader = new Mock<IInfrastructureConfigurationLoader>();
            mockConfigLoader.Setup(x => x.Load(It.IsAny<string>())).Returns(new InfrastructureConfigurations
            {
                sql_Connection = "Mocked Connection"
            });

            RepositoryFactory factory = new RepositoryFactory(mockConfigLoader.Object);

            IRepository<DtoConfiguration> userRepo = factory.CreateConfigurationsRepository();
            IReadOnlyRepository<DtoConfiguration> userRepoReadOnly = factory.CreateReadOnlyConfigurationsRepository();

            Assert.IsInstanceOfType(userRepo, typeof(ConfigurationsRepository));
            Assert.IsInstanceOfType(userRepoReadOnly, typeof(ConfigurationsRepository));
        }
    }
}
