using IntimateDesires.Infrastructure.Repositories;
using IntimateDesires.Infrastructure.Repositories.DataTableObjects;
using IntimateDesires.Infrastructure.Repositories.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Infrastructure.Tests.Repositories.EntityFramework
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

            Assert.IsInstanceOfType(userRepo, typeof(UserRepository));
        }
    }
}
