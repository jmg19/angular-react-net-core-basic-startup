using BaseStartupProject.Application.AppModels.ResultModels;
using BaseStartupProject.Application.Queries.Users;
using BaseStartupProject.Application.Services.User;
using BaseStartupProject.Infrastructure.Repositories;
using BaseStartupProject.Infrastructure.Repositories.DataTableObjects;
using BaseStartupProject.Infrastructure.Repositories.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Tests.Application.Services
{
    [TestClass]
    public class UserQueriesServiceTests
    {
        [TestMethod]
        public void GetAllUsers_Test()
        {
            #region === ARRANGE ===

            Mock<IReadOnlyRepository<DtoUser>> mockReadOnlyRepository = new Mock<IReadOnlyRepository<DtoUser>>();
            Mock<IRepositoryFactory> mockRepositoryFactory = new Mock<IRepositoryFactory>();

            mockReadOnlyRepository.Setup(x => x.GetAll()).Returns(new List<DtoUser>() { new DtoUser(999, "", "", false) });
            mockRepositoryFactory.Setup(x => x.CreateReadOnlyUsersRepository()).Returns(mockReadOnlyRepository.Object);

            UserQueriesService userQueriesService = new UserQueriesService(mockRepositoryFactory.Object);

            #endregion

            #region === ACT ===

            IList<UserAppModel> users = userQueriesService.GetAllUsers();

            #endregion

            #region === ASSERT ===

            mockReadOnlyRepository.Verify(x => x.GetAll(), Times.Once);
            Assert.AreEqual<int>(1, users.Count);
            Assert.AreEqual<int>(999, users[0].id);

            #endregion
        }

        [TestMethod]
        public void GetUser_Test()
        {
            #region === ARRANGE ===

            Mock<IReadOnlyRepository<DtoUser>> mockReadOnlyRepository = new Mock<IReadOnlyRepository<DtoUser>>();
            Mock<IRepositoryFactory> mockRepositoryFactory = new Mock<IRepositoryFactory>();

            mockReadOnlyRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new DtoUser(999, "", "", false));
            mockRepositoryFactory.Setup(x => x.CreateReadOnlyUsersRepository()).Returns(mockReadOnlyRepository.Object);

            UserQueriesService userQueriesService = new UserQueriesService(mockRepositoryFactory.Object);

            #endregion

            #region === ACT ===

            UserAppModel user = userQueriesService.GetUser(999);

            #endregion

            #region === ASSERT ===

            mockReadOnlyRepository.Verify(x => x.Get(It.IsAny<int>()), Times.Once);            
            Assert.AreEqual<int>(999, user.id);

            #endregion
        }

        [TestMethod]
        public void GetUsers_Test()
        {
            #region === ARRANGE ===

            Mock<IReadOnlyRepository<DtoUser>> mockReadOnlyRepository = new Mock<IReadOnlyRepository<DtoUser>>();
            Mock<IRepositoryFactory> mockRepositoryFactory = new Mock<IRepositoryFactory>();

            mockReadOnlyRepository.Setup(x => x.Get(It.IsAny<SearchRule[]>(), It.IsAny<OrderingRule[]>())).Returns(new List<DtoUser>() { new DtoUser(999, "", "", false) });
            mockRepositoryFactory.Setup(x => x.CreateReadOnlyUsersRepository()).Returns(mockReadOnlyRepository.Object);

            UserQueriesService userQueriesService = new UserQueriesService(mockRepositoryFactory.Object);

            #endregion

            #region === ACT ===

            IList<UserAppModel> users = userQueriesService.GetUsers(new SearchRule[] { new SearchRule {condition = "some stuff" }}, new OrderingRule[] { new OrderingRule { fieldName = "SomeField" } });

            #endregion

            #region === ASSERT ===

            mockReadOnlyRepository.Verify(x => x.Get(It.IsAny<SearchRule[]>(), It.IsAny<OrderingRule[]>()), Times.Once);
            Assert.AreEqual<int>(1, users.Count);
            Assert.AreEqual<int>(999, users[0].id);

            #endregion
        }

        [TestMethod]
        public void GetUsers_UserQuery_Test()
        {
            #region === ARRANGE ===

            Mock<IReadOnlyRepository<DtoUser>> mockReadOnlyRepository = new Mock<IReadOnlyRepository<DtoUser>>();
            Mock<IRepositoryFactory> mockRepositoryFactory = new Mock<IRepositoryFactory>();

            mockReadOnlyRepository.Setup(x => x.Get(It.IsAny<SearchRule[]>(), It.IsAny<OrderingRule[]>())).Returns(new List<DtoUser>() { new DtoUser(999, "", "", false) });
            mockRepositoryFactory.Setup(x => x.CreateReadOnlyUsersRepository()).Returns(mockReadOnlyRepository.Object);

            UserQueriesService userQueriesService = new UserQueriesService(mockRepositoryFactory.Object);
            UserQuery query = new UserQuery()
            {
                search_rules = new SearchRule[] { new SearchRule { condition = "some stuff" } },
                ordering_rules = new OrderingRule[] { new OrderingRule { fieldName = "SomeField" } }
            };

            #endregion

            #region === ACT ===

            IList<UserAppModel> users = userQueriesService.GetUsers(query);

            #endregion

            #region === ASSERT ===

            mockReadOnlyRepository.Verify(x => x.Get(It.IsAny<SearchRule[]>(), It.IsAny<OrderingRule[]>()), Times.Once);
            Assert.AreEqual<int>(1, users.Count);
            Assert.AreEqual<int>(999, users[0].id);

            #endregion
        }

        [TestMethod]
        public void GetUsers_UserByUsernameQuery_Test()
        {
            #region === ARRANGE ===

            Mock<IReadOnlyRepository<DtoUser>> mockReadOnlyRepository = new Mock<IReadOnlyRepository<DtoUser>>();
            Mock<IRepositoryFactory> mockRepositoryFactory = new Mock<IRepositoryFactory>();

            mockReadOnlyRepository.Setup(x => x.Get(It.IsAny<SearchRule[]>(), It.IsAny<OrderingRule[]>())).Returns(new List<DtoUser>() { new DtoUser(999, "", "", false) });
            mockRepositoryFactory.Setup(x => x.CreateReadOnlyUsersRepository()).Returns(mockReadOnlyRepository.Object);

            UserQueriesService userQueriesService = new UserQueriesService(mockRepositoryFactory.Object);
            UserQuery query = new UserByUsernameQuery("Some User");

            #endregion

            #region === ACT ===

            IList<UserAppModel> users = userQueriesService.GetUsers(query);

            #endregion

            #region === ASSERT ===

            mockReadOnlyRepository.Verify(x => x.Get(It.IsAny<SearchRule[]>(), It.IsAny<OrderingRule[]>()), Times.Once);
            Assert.AreEqual<int>(1, users.Count);
            Assert.AreEqual<int>(999, users[0].id);

            #endregion
        }
    }
}
