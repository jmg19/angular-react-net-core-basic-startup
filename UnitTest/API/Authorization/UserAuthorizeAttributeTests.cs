using BaseStartupProject.API.Authorization;
using BaseStartupProject.Application.AppModels.ResultModels;
using BaseStartupProject.Application.Services;
using BaseStartupProject.Application.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Tests.API.Authorization
{
    [TestClass]
    public class UserAuthorizeAttributeTests
    {
        [TestMethod]
        public void OnAuthorization_Test_Missing_Headers()
        {
            #region === ARRANGE ===

            Mock<IList<IFilterMetadata>> mockListIFilterMetadata = new Mock<IList<IFilterMetadata>>();
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
            Mock<ISessionTokenService> mockSessionTokenService = new Mock<ISessionTokenService>();
            UserAuthorizeAttribute userAuthorizeAttribute = new UserAuthorizeAttribute(mockApplicationServicesFactory.Object);

            UserAppModel userAppModel = new UserAppModel() { id = 55, active = true, username = "User" };

            mockSessionTokenService.Setup(x => x.DecryptToken(It.IsAny<string>(), It.IsAny<string>())).Returns(userAppModel);
            mockApplicationServicesFactory.Setup(x => x.CreateSessionTokenService()).Returns(mockSessionTokenService.Object);

            ActionContext actionContext = new ActionContext(mockContext.Object, new RouteData(), new ControllerActionDescriptor());

            #endregion

            #region === ACT ===

            AuthorizationFilterContext authorizationFilterContext = new AuthorizationFilterContext(actionContext, mockListIFilterMetadata.Object);
            userAuthorizeAttribute.OnAuthorization(authorizationFilterContext);

            #endregion

            #region === ASSERT ===

            Assert.IsInstanceOfType(authorizationFilterContext.Result, typeof(UnauthorizedResult));

            #endregion
        }

        [TestMethod]
        public void OnAuthorization_Test_Not_Loged_User()
        {
            #region === ARRANGE ===

            Mock<IList<IFilterMetadata>> mockListIFilterMetadata = new Mock<IList<IFilterMetadata>>();
            Mock<HttpRequest> mockRequest = new Mock<HttpRequest>();
            Mock<IHeaderDictionary> mockHeaders = new Mock<IHeaderDictionary>();

            mockRequest.SetupGet(x => x.Headers).Returns(
                new HeaderDictionary {
                    {"UUID", "Some UUID"},
                    {"Token", "Some Token"}
                }
            );

            Mock<HttpContext> mockContext = new Mock<HttpContext>();
            mockContext.SetupGet(x => x.Request).Returns(mockRequest.Object);

            Mock<IApplicationServicesFactory> mockApplicationServicesFactory = new Mock<IApplicationServicesFactory>();
            Mock<ISessionTokenService> mockSessionTokenService = new Mock<ISessionTokenService>();
            UserAuthorizeAttribute userAuthorizeAttribute = new UserAuthorizeAttribute(mockApplicationServicesFactory.Object);

            UserAppModel userAppModel = new UserAppModel() { id = 0, active = true, username = "User" };

            mockSessionTokenService.Setup(x => x.DecryptToken(It.IsAny<string>(), It.IsAny<string>())).Returns(userAppModel);
            mockApplicationServicesFactory.Setup(x => x.CreateSessionTokenService()).Returns(mockSessionTokenService.Object);

            ActionContext actionContext = new ActionContext(mockContext.Object, new RouteData(), new ControllerActionDescriptor());

            #endregion

            #region === ACT ===

            AuthorizationFilterContext authorizationFilterContext = new AuthorizationFilterContext(actionContext, mockListIFilterMetadata.Object);
            userAuthorizeAttribute.OnAuthorization(authorizationFilterContext);

            #endregion

            #region === ASSERT ===

            Assert.IsInstanceOfType(authorizationFilterContext.Result, typeof(UnauthorizedResult));

            #endregion
        }

        [TestMethod]
        public void OnAuthorization_Test_Loged_User()
        {
            #region === ARRANGE ===

            Mock<IList<IFilterMetadata>> mockListIFilterMetadata = new Mock<IList<IFilterMetadata>>();
            Mock<HttpRequest> mockRequest = new Mock<HttpRequest>();
            Mock<IHeaderDictionary> mockHeaders = new Mock<IHeaderDictionary>();

            mockRequest.SetupGet(x => x.Headers).Returns(
                new HeaderDictionary {
                    {"UUID", "Some UUID"},
                    {"Token", "Some Token"}
                }
            );

            Mock<HttpContext> mockContext = new Mock<HttpContext>();
            mockContext.SetupGet(x => x.Request).Returns(mockRequest.Object);

            Mock<IApplicationServicesFactory> mockApplicationServicesFactory = new Mock<IApplicationServicesFactory>();
            Mock<ISessionTokenService> mockSessionTokenService = new Mock<ISessionTokenService>();
            UserAuthorizeAttribute userAuthorizeAttribute = new UserAuthorizeAttribute(mockApplicationServicesFactory.Object);

            UserAppModel userAppModel = new UserAppModel() { id = 55, active = true, username = "User" };

            mockSessionTokenService.Setup(x => x.DecryptToken(It.IsAny<string>(), It.IsAny<string>())).Returns(userAppModel);
            mockApplicationServicesFactory.Setup(x => x.CreateSessionTokenService()).Returns(mockSessionTokenService.Object);

            ActionContext actionContext = new ActionContext(mockContext.Object, new RouteData(), new ControllerActionDescriptor());

            #endregion

            #region === ACT ===

            AuthorizationFilterContext authorizationFilterContext = new AuthorizationFilterContext(actionContext, mockListIFilterMetadata.Object);
            userAuthorizeAttribute.OnAuthorization(authorizationFilterContext);

            #endregion

            #region === ASSERT ===

            Assert.IsNotInstanceOfType(authorizationFilterContext.Result, typeof(UnauthorizedResult));

            #endregion
        }
    }
}
