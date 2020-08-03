using BaseStartupProject.Application.InfrastructureOperations;
using BaseStartupProject.Application.InfrastructureOperations.DataBaseOperations;
using BaseStartupProject.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BaseStartupProject.Tests.Application.InfrastructureOperations
{
    [TestClass]
    public class InfrastructureOperationsFactoryTests
    {        
        [TestMethod]
        public void SaveChanges_Test() 
        {
            #region === ARRANGE ===

            Mock<IRepositoryFactory> repositoryFactoryMock = new Mock<IRepositoryFactory>();
            DataBaseOperationsFactory dataBaseOperationsFactory = new DataBaseOperationsFactory();

            InfrastructureOperationsFactory factory = new InfrastructureOperationsFactory(repositoryFactoryMock.Object, dataBaseOperationsFactory);

            #endregion

            #region === ACT ===

            factory.SaveChanges();

            #endregion

            #region === ASSERT ===

            repositoryFactoryMock.Verify(x => x.SaveChances(), Times.Once);

            #endregion
        }
    }
}
