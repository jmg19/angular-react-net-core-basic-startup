using BaseStartupProject.API.Controllers;
using BaseStartupProject.Application.AppModels.ResultModels;
using BaseStartupProject.Application.Services;
using BaseStartupProject.Application.Services.User;
using BaseStartupProject.Infrastructure.Repositories;
using BaseStartupProject.Infrastructure.Repositories.DataTableObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Tests.API.Controllers
{
    [TestClass]
    public class SessionControllerTests
    {
        [TestMethod]
        public void NewToken_test() {
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
            
            Mock<IRepositoryFactory> mockRepositoryFactory = new Mock<IRepositoryFactory>();
            Mock<IReadOnlyRepository<DtoConfiguration>> mockReadOnlyRepository = new Mock<IReadOnlyRepository<DtoConfiguration>>();
            Mock<ILogger<SessionController>> mockLoger = new Mock<ILogger<SessionController>>();
            SessionController sessionController = new SessionController(mockLoger.Object, mockApplicationServicesFactory.Object);
            UserAppModel userAppModel = new UserAppModel() { id = 55, active = true, username = "User" };

            mockRepositoryFactory.Setup(x => x.CreateReadOnlyConfigurationsRepository()).Returns(mockReadOnlyRepository.Object);
            mockReadOnlyRepository.Setup(x => x.GetAll()).Returns(new DtoConfiguration[] { new DtoConfiguration(1, "tokenEncriptKey", "ABC") });
            mockApplicationServicesFactory.Setup(x => x.CreateSessionTokenService()).Returns(new SessionTokenService(mockRepositoryFactory.Object));

            sessionController.ControllerContext = new ControllerContext(new ActionContext(mockContext.Object, new RouteData(), new ControllerActionDescriptor()));

            #endregion

            #region === ACT ===

            string result = sessionController.NewToken();

            #endregion

            #region === ASSERT ===

            Assert.AreNotEqual(true, string.IsNullOrEmpty(result));

            #endregion
        }
    }
}
