using BaseStartupProject.Application.InfrastructureOperations;
using BaseStartupProject.Application.InfrastructureOperations.DataBaseOperations;
using BaseStartupProject.Business;
using BaseStartupProject.Business.Events;
using BaseStartupProject.Infrastructure.Repositories;
using BaseStartupProject.Infrastructure.Repositories.DataTableObjects;
using BaseStartupProject.Infrastructure.Repositories.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseStartupProject.Tests.Application.InfrastructureOperations.DataBaseOperations
{
    [TestClass]
    public class UsersTableOperationsTests
    {
        [TestMethod]
        public void CreateDalConsultOperation_For_User_Get_Test()
        {
            #region === ARRANGE ===

            DtoUser dtoUser = new DtoUser(999, "", "", false);
            Mock<IReadOnlyRepository<DtoUser>> repositoryMock = new Mock<IReadOnlyRepository<DtoUser>>();
            repositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(dtoUser);
            Mock<IRepositoryFactory> repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(x => x.CreateReadOnlyUsersRepository()).Returns(repositoryMock.Object);

            DataBaseOperationsFactory dataBaseOperationsFactory = new DataBaseOperationsFactory();
            Mock<BusinessObject> businessObjectMock = new Mock<BusinessObject>(null);

            BusinessConsultEventArgs args = new BusinessConsultEventArgs(typeof(User)) { businessConsultType = BusinessConsultType.Get, entityId = 1 };
            InfrastructureOperationsFactory factory = new InfrastructureOperationsFactory(repositoryFactoryMock.Object, dataBaseOperationsFactory);

            #endregion

            #region === ACT ===

            IInfrastructureOperation operation = factory.CreateDalConsultOperations(businessObjectMock.Object, args);
            operation.Execute();
            User result = (User)(args.result.FirstOrDefault());

            #endregion

            #region === ASSERT ===

            repositoryMock.Verify(mock => mock.Get(It.IsAny<int>()), Times.Once());
            Assert.AreEqual(dtoUser.ID, result.id);

            #endregion
        }

        [TestMethod]
        public void CreateDalConsultOperation_For_User_GetAll_Test()
        {
            #region === ARRANGE ===

            DtoUser dtoUser = new DtoUser(999, "", "", false);
            List<DtoUser> getMockResult = new List<DtoUser>() { dtoUser, dtoUser, dtoUser, dtoUser, dtoUser };
            int expected_length = getMockResult.Count;

            Mock<IReadOnlyRepository<DtoUser>> repositoryMock = new Mock<IReadOnlyRepository<DtoUser>>();
            repositoryMock.Setup(x => x.GetAll()).Returns(getMockResult);
            Mock<IRepositoryFactory> repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(x => x.CreateReadOnlyUsersRepository()).Returns(repositoryMock.Object);

            DataBaseOperationsFactory dataBaseOperationsFactory = new DataBaseOperationsFactory();
            Mock<BusinessObject> businessObjectMock = new Mock<BusinessObject>(null);

            BusinessConsultEventArgs args = new BusinessConsultEventArgs(typeof(User)) { businessConsultType = BusinessConsultType.GetAll };
            InfrastructureOperationsFactory factory = new InfrastructureOperationsFactory(repositoryFactoryMock.Object, dataBaseOperationsFactory);

            #endregion

            #region === ACT ===

            IInfrastructureOperation operation = factory.CreateDalConsultOperations(businessObjectMock.Object, args);
            operation.Execute();
            IEnumerable<User> result = (IEnumerable<User>)(args.result);

            #endregion

            #region === ASSERT ===

            repositoryMock.Verify(mock => mock.GetAll(), Times.Once());
            Assert.AreEqual(expected_length, result.Count());

            #endregion
        }

        [TestMethod]
        public void CreateDalConsultOperation_For_User_GetBy_Test()
        {
            #region === ARRANGE ===

            DtoUser dtoUser = new DtoUser(999, "", "", false);
            List<DtoUser> getMockResult = new List<DtoUser>() { dtoUser, dtoUser, dtoUser, dtoUser, dtoUser };
            int expected_length = getMockResult.Count;

            Mock<IReadOnlyRepository<DtoUser>> repositoryMock = new Mock<IReadOnlyRepository<DtoUser>>();
            repositoryMock.Setup(x => x.Get(It.IsAny<SearchRule[]>(), It.IsAny<OrderingRule[]>())).Returns(getMockResult);
            Mock<IRepositoryFactory> repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(x => x.CreateReadOnlyUsersRepository()).Returns(repositoryMock.Object);

            DataBaseOperationsFactory dataBaseOperationsFactory = new DataBaseOperationsFactory();
            Mock<BusinessObject> businessObjectMock = new Mock<BusinessObject>(null);

            BusinessConsultEventArgs args = new BusinessConsultEventArgs(typeof(User)) { businessConsultType = BusinessConsultType.GetBy, conditions = new string[] { } };
            InfrastructureOperationsFactory factory = new InfrastructureOperationsFactory(repositoryFactoryMock.Object, dataBaseOperationsFactory);

            #endregion

            #region === ACT ===

            IInfrastructureOperation operation = factory.CreateDalConsultOperations(businessObjectMock.Object, args);
            operation.Execute();
            IEnumerable<User> result = (IEnumerable<User>)(args.result);

            #endregion

            #region === ASSERT ===

            repositoryMock.Verify(mock => mock.Get(It.IsAny<SearchRule[]>(), It.IsAny<OrderingRule[]>()), Times.Once());
            Assert.AreEqual(expected_length, result.Count());

            #endregion
        }

        [TestMethod]
        public void CreateDalChangeOperations_For_User_Add_Test()
        {
            #region === ARRANGE ===

            Mock<IRepository<DtoUser>> repositoryMock = new Mock<IRepository<DtoUser>>();
            Mock<IRepositoryFactory> repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(x => x.CreateUsersRepository()).Returns(repositoryMock.Object);
            repositoryMock.Setup(x => x.Add(It.IsAny<DtoUser>())).Returns(new DtoUser());

            DataBaseOperationsFactory dataBaseOperationsFactory = new DataBaseOperationsFactory();
            Mock<BusinessObject> businessObjectMock = new Mock<BusinessObject>(null);

            BusinessChangeEventArgs args = new BusinessChangeEventArgs(typeof(User)) { businessChangeType = BusinessChangeType.Add, entity = new User("", null) };
            InfrastructureOperationsFactory factory = new InfrastructureOperationsFactory(repositoryFactoryMock.Object, dataBaseOperationsFactory);

            #endregion

            #region === ACT ===

            IInfrastructureOperation operation = factory.CreateDalChangeOperations(businessObjectMock.Object, args);
            operation.Execute();

            #endregion

            #region === ASSERT ===

            repositoryMock.Verify(mock => mock.Add(It.IsAny<DtoUser>()), Times.Once());

            #endregion
        }

        [TestMethod]
        public void CreateDalChangeOperations_For_User_Update_Test()
        {
            #region === ARRANGE ===

            Mock<IRepository<DtoUser>> repositoryMock = new Mock<IRepository<DtoUser>>();
            Mock<IRepositoryFactory> repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(x => x.CreateUsersRepository()).Returns(repositoryMock.Object);

            DataBaseOperationsFactory dataBaseOperationsFactory = new DataBaseOperationsFactory();
            Mock<BusinessObject> businessObjectMock = new Mock<BusinessObject>(null);

            BusinessChangeEventArgs args = new BusinessChangeEventArgs(typeof(User)) { businessChangeType = BusinessChangeType.Update, entity = new User("", null) };
            InfrastructureOperationsFactory factory = new InfrastructureOperationsFactory(repositoryFactoryMock.Object, dataBaseOperationsFactory);

            #endregion

            #region === ACT ===

            IInfrastructureOperation operation = factory.CreateDalChangeOperations(businessObjectMock.Object, args);
            operation.Execute();

            #endregion

            #region === ASSERT ===

            repositoryMock.Verify(mock => mock.Update(It.IsAny<DtoUser>()), Times.Once());

            #endregion
        }

        [TestMethod]
        public void CreateDalChangeOperations_For_User_Delete_Test()
        {
            #region === ARRANGE ===

            Mock<IRepository<DtoUser>> repositoryMock = new Mock<IRepository<DtoUser>>();
            Mock<IRepositoryFactory> repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(x => x.CreateUsersRepository()).Returns(repositoryMock.Object);

            DataBaseOperationsFactory dataBaseOperationsFactory = new DataBaseOperationsFactory();
            Mock<BusinessObject> businessObjectMock = new Mock<BusinessObject>(null);

            BusinessChangeEventArgs args = new BusinessChangeEventArgs(typeof(User)) { businessChangeType = BusinessChangeType.Delete, entity = new User("", null) };
            InfrastructureOperationsFactory factory = new InfrastructureOperationsFactory(repositoryFactoryMock.Object, dataBaseOperationsFactory);

            #endregion

            #region === ACT ===

            IInfrastructureOperation operation = factory.CreateDalChangeOperations(businessObjectMock.Object, args);
            operation.Execute();

            #endregion

            #region === ASSERT ===

            repositoryMock.Verify(mock => mock.Delete(It.IsAny<int>()), Times.Once());

            #endregion
        }
    }
}
