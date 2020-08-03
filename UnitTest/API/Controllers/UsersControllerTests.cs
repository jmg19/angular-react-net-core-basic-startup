using BaseStartupProject.API.Controllers;
using BaseStartupProject.Application.AppModels.ResultModels;
using BaseStartupProject.Application.Commands.Users;
using BaseStartupProject.Application.Queries.Users;
using BaseStartupProject.Application.Services;
using BaseStartupProject.Application.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseStartupProject.Tests.API.Controllers
{
    [TestClass]
    public class UsersControllerTests
    {
        [TestMethod]
        public void Get_All_test()
        {
            #region === ARRANGE ===

            Mock<IApplicationServicesFactory> mockApplicationServicesFactory = new Mock<IApplicationServicesFactory>();
            Mock<IUserQueriesService> mockUserQueriesService = new Mock<IUserQueriesService>();
            Mock<ILogger<AccountsController>> mockLoger = new Mock<ILogger<AccountsController>>();
            AccountsController usersController = new AccountsController(mockLoger.Object, mockApplicationServicesFactory.Object);
            List<UserAppModel> userAppModels = new List<UserAppModel>();
            userAppModels.Add(new UserAppModel() { id = 55, active = true, username = "User" });

            mockApplicationServicesFactory.Setup(x => x.CreateUserQueriesService()).Returns(mockUserQueriesService.Object);

            mockUserQueriesService.Setup(x => x.GetAllUsers()).Returns(userAppModels);

            #endregion

            #region === ACT ===

            var result = usersController.Get();

            #endregion

            #region === ASSERT ===

            Assert.AreEqual<int>(userAppModels.Count, result.Count());

            #endregion
        }

        [TestMethod]
        public void Get_ById_Test()
        {
            #region === ARRANGE ===

            Mock<IApplicationServicesFactory> mockApplicationServicesFactory = new Mock<IApplicationServicesFactory>();
            Mock<IUserQueriesService> mockUserQueriesService = new Mock<IUserQueriesService>();
            Mock<ILogger<AccountsController>> mockLoger = new Mock<ILogger<AccountsController>>();
            AccountsController usersController = new AccountsController(mockLoger.Object, mockApplicationServicesFactory.Object);
            UserAppModel userAppModel = new UserAppModel() { id = 55, active = true, username = "User" };

            mockApplicationServicesFactory.Setup(x => x.CreateUserQueriesService()).Returns(mockUserQueriesService.Object);

            mockUserQueriesService.Setup(x => x.GetUser(It.IsAny<int>())).Returns(userAppModel);

            #endregion

            #region === ACT ===

            var result = usersController.Get(55);

            #endregion

            #region === ASSERT ===

            Assert.AreEqual<int>(userAppModel.id, result.id);
            Assert.AreEqual<bool>(userAppModel.active, result.active);
            Assert.AreEqual<string>(userAppModel.username, result.username);

            #endregion
        }

        [TestMethod]
        public void Get_ByUserName_Test()
        {
            #region === ARRANGE ===

            Mock<IApplicationServicesFactory> mockApplicationServicesFactory = new Mock<IApplicationServicesFactory>();
            Mock<IUserQueriesService> mockUserQueriesService = new Mock<IUserQueriesService>();
            Mock<ILogger<AccountsController>> mockLoger = new Mock<ILogger<AccountsController>>();
            AccountsController usersController = new AccountsController(mockLoger.Object, mockApplicationServicesFactory.Object);
            List<UserAppModel> userAppModels = new List<UserAppModel>();
            userAppModels.Add(new UserAppModel() { id = 55, active = true, username = "User" });

            mockApplicationServicesFactory.Setup(x => x.CreateUserQueriesService()).Returns(mockUserQueriesService.Object);

            mockUserQueriesService.Setup(x => x.GetUsers(It.IsAny<UserQuery>())).Returns(userAppModels);

            #endregion

            #region === ACT ===

            var result = usersController.Get("User");

            #endregion

            #region === ASSERT ===

            Assert.AreEqual<int>(userAppModels[0].id, result.id);
            Assert.AreEqual<bool>(userAppModels[0].active, result.active);
            Assert.AreEqual<string>(userAppModels[0].username, result.username);

            #endregion
        }

        [TestMethod]
        public void CheckExists_Test()
        {
            #region === ARRANGE ===

            Mock<IApplicationServicesFactory> mockApplicationServicesFactory = new Mock<IApplicationServicesFactory>();
            Mock<IUserQueriesService> mockUserQueriesService = new Mock<IUserQueriesService>();
            Mock<ILogger<AccountsController>> mockLoger = new Mock<ILogger<AccountsController>>();
            AccountsController usersController = new AccountsController(mockLoger.Object, mockApplicationServicesFactory.Object);
            List<UserAppModel> userAppModels = new List<UserAppModel>();
            userAppModels.Add(new UserAppModel() { id = 55, active = true, username = "User" });

            mockApplicationServicesFactory.Setup(x => x.CreateUserQueriesService()).Returns(mockUserQueriesService.Object);

            mockUserQueriesService.Setup(x => x.GetUsers(It.IsAny<UserQuery>())).Returns(userAppModels);

            #endregion

            #region === ACT ===

            var result = usersController.CheckExists("User");

            #endregion

            #region === ASSERT ===

            Assert.AreEqual<bool>(true, result);

            #endregion
        }

        [TestMethod]
        public void Get_ByUserQuery_Test()
        {
            #region === ARRANGE ===

            Mock<IApplicationServicesFactory> mockApplicationServicesFactory = new Mock<IApplicationServicesFactory>();
            Mock<IUserQueriesService> mockUserQueriesService = new Mock<IUserQueriesService>();
            Mock<ILogger<AccountsController>> mockLoger = new Mock<ILogger<AccountsController>>();
            AccountsController usersController = new AccountsController(mockLoger.Object, mockApplicationServicesFactory.Object);
            List<UserAppModel> userAppModels = new List<UserAppModel>();
            userAppModels.Add(new UserAppModel() { id = 55, active = true, username = "User" });

            mockApplicationServicesFactory.Setup(x => x.CreateUserQueriesService()).Returns(mockUserQueriesService.Object);

            mockUserQueriesService.Setup(x => x.GetUsers(It.IsAny<UserQuery>())).Returns(userAppModels);

            #endregion

            #region === ACT ===

            var result = usersController.Get(new UserQuery());

            #endregion

            #region === ASSERT ===

            Assert.AreEqual<int>(userAppModels.Count, result.Count());

            #endregion
        }

        [TestMethod]
        public void Post_Test()
        {
            #region === ARRANGE ===

            Mock<IApplicationServicesFactory> mockApplicationServicesFactory = new Mock<IApplicationServicesFactory>();
            Mock<IUserCommandsService> mockUserCommandsService = new Mock<IUserCommandsService>();
            Mock<ILogger<AccountsController>> mockLoger = new Mock<ILogger<AccountsController>>();
            AccountsController usersController = new AccountsController(mockLoger.Object, mockApplicationServicesFactory.Object);            

            mockApplicationServicesFactory.Setup(x => x.CreateUserCommandsService()).Returns(mockUserCommandsService.Object);

            #endregion

            #region === ACT ===

            usersController.Post(new SignUpUser());

            #endregion

            #region === ASSERT ===

            mockUserCommandsService.Verify(mock => mock.SignUpNewUser(It.IsAny<SignUpUser>()), Times.Once());

            #endregion
        }

        [TestMethod]
        public void Login_Test()
        {
            #region === ARRANGE ===

            Mock<HttpRequest> mockRequest = new Mock<HttpRequest>();
            Mock<IHeaderDictionary> mockHeaders = new Mock<IHeaderDictionary>();

            mockRequest.SetupGet(x => x.Headers).Returns(
                new HeaderDictionary {
                    {"UUID", "Some UUID"}
                }
            );

            Mock<HttpContext> mockContext = new Mock<HttpContext>();
            mockContext.SetupGet(x => x.Request).Returns(mockRequest.Object);

            Mock<IApplicationServicesFactory> mockApplicationServicesFactory = new Mock<IApplicationServicesFactory>();
            Mock<IUserCommandsService> mockUserCommandsService = new Mock<IUserCommandsService>();
            Mock<ISessionTokenService> mockSessionTokenService = new Mock<ISessionTokenService>();
            Mock<ILogger<AccountsController>> mockLoger = new Mock<ILogger<AccountsController>>();
            AccountsController usersController = new AccountsController(mockLoger.Object, mockApplicationServicesFactory.Object);
            usersController.ControllerContext = new ControllerContext(new ActionContext(mockContext.Object, new RouteData(), new ControllerActionDescriptor()));
            string expectedToken = "Novo TOKEN!";

            mockApplicationServicesFactory.Setup(x => x.CreateUserCommandsService()).Returns(mockUserCommandsService.Object);
            mockApplicationServicesFactory.Setup(x => x.CreateSessionTokenService()).Returns(mockSessionTokenService.Object);
            mockUserCommandsService.Setup(x => x.Login(It.IsAny<UserLogin>())).Returns(5);
            mockSessionTokenService.Setup(x => x.GenerateToken(It.IsAny<string>(), It.IsAny<UserAppModel>(), It.IsAny<bool>())).Returns(expectedToken);

            #endregion

            #region === ACT ===

            string token = usersController.Login(new UserLogin());

            #endregion

            #region === ASSERT ===

            Assert.AreEqual<string>(expectedToken, token);

            #endregion
        }

        [TestMethod]
        public void Activate_Test()
        {
            #region === ARRANGE ===

            Mock<IApplicationServicesFactory> mockApplicationServicesFactory = new Mock<IApplicationServicesFactory>();
            Mock<IUserCommandsService> mockUserCommandsService = new Mock<IUserCommandsService>();
            Mock<ILogger<AccountsController>> mockLoger = new Mock<ILogger<AccountsController>>();
            AccountsController usersController = new AccountsController(mockLoger.Object, mockApplicationServicesFactory.Object);            

            mockApplicationServicesFactory.Setup(x => x.CreateUserCommandsService()).Returns(mockUserCommandsService.Object);

            #endregion

            #region === ACT ===

            usersController.Activate(55);

            #endregion

            #region === ASSERT ===

            mockUserCommandsService.Verify(mock => mock.Activate(It.IsAny<ActivateUser>()), Times.Once());

            #endregion
        }

        [TestMethod]
        public void Inactivate_Test()
        {
            #region === ARRANGE ===

            Mock<IApplicationServicesFactory> mockApplicationServicesFactory = new Mock<IApplicationServicesFactory>();
            Mock<IUserCommandsService> mockUserCommandsService = new Mock<IUserCommandsService>();
            Mock<ILogger<AccountsController>> mockLoger = new Mock<ILogger<AccountsController>>();
            AccountsController usersController = new AccountsController(mockLoger.Object, mockApplicationServicesFactory.Object);

            mockApplicationServicesFactory.Setup(x => x.CreateUserCommandsService()).Returns(mockUserCommandsService.Object);

            #endregion

            #region === ACT ===

            usersController.Inactivate(55);

            #endregion

            #region === ASSERT ===

            mockUserCommandsService.Verify(mock => mock.Inactivate(It.IsAny<InactivateUser>()), Times.Once());

            #endregion
        }
    }
}
