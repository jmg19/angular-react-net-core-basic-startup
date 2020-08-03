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
    public class BaseAuthorizeAttributeTest
    {
        [TestMethod]
        public void OnAuthorization_Test_NeedToken()
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
            BaseAuthorizeAttribute baseAuthorizeAttribute = new BaseAuthorizeAttribute(mockApplicationServicesFactory.Object);            
            
            UserAppModel userAppModel = new UserAppModel() { id = 55, active = true, username = "User" };
            
            mockSessionTokenService.Setup(x => x.DecryptToken(It.IsAny<string>(), It.IsAny<string>())).Returns(userAppModel);
            mockApplicationServicesFactory.Setup(x => x.CreateSessionTokenService()).Returns(mockSessionTokenService.Object);

            ActionContext actionContext = new ActionContext(mockContext.Object, new RouteData(), new ControllerActionDescriptor());

            #endregion

            #region === ACT ===

            AuthorizationFilterContext authorizationFilterContext = new AuthorizationFilterContext(actionContext, mockListIFilterMetadata.Object);
            baseAuthorizeAttribute.OnAuthorization(authorizationFilterContext);

            #endregion

            #region === ASSERT ===

            Assert.IsInstanceOfType(authorizationFilterContext.Result, typeof(UnauthorizedResult));

            #endregion
        }

        [TestMethod]
        public void OnAuthorization_Test_NeedToken_And_I_Send_Token()
        {
            #region === ARRANGE ===

            Mock<IList<IFilterMetadata>> mockListIFilterMetadata = new Mock<IList<IFilterMetadata>>();
            Mock<HttpRequest> mockRequest = new Mock<HttpRequest>();
            Mock<IHeaderDictionary> mockHeaders = new Mock<IHeaderDictionary>();

            mockRequest.SetupGet(x => x.Headers).Returns(
                new HeaderDictionary {
                    {"UUID", "Some UUID"},
                    {"Token", "Some Token" }
                }
            );

            Mock<HttpContext> mockContext = new Mock<HttpContext>();
            mockContext.SetupGet(x => x.Request).Returns(mockRequest.Object);

            Mock<IApplicationServicesFactory> mockApplicationServicesFactory = new Mock<IApplicationServicesFactory>();
            Mock<ISessionTokenService> mockSessionTokenService = new Mock<ISessionTokenService>();
            BaseAuthorizeAttribute baseAuthorizeAttribute = new BaseAuthorizeAttribute(mockApplicationServicesFactory.Object);

            UserAppModel userAppModel = new UserAppModel() { id = 55, active = true, username = "User" };

            mockSessionTokenService.Setup(x => x.DecryptToken(It.IsAny<string>(), It.IsAny<string>())).Returns(userAppModel);
            mockApplicationServicesFactory.Setup(x => x.CreateSessionTokenService()).Returns(mockSessionTokenService.Object);

            ActionContext actionContext = new ActionContext(mockContext.Object, new RouteData(), new ControllerActionDescriptor());

            #endregion

            #region === ACT ===

            AuthorizationFilterContext authorizationFilterContext = new AuthorizationFilterContext(actionContext, mockListIFilterMetadata.Object);
            baseAuthorizeAttribute.OnAuthorization(authorizationFilterContext);

            #endregion

            #region === ASSERT ===

            Assert.IsNotInstanceOfType(authorizationFilterContext.Result, typeof(UnauthorizedResult));

            #endregion
        }

        [TestMethod]
        public void OnAuthorization_Test_DontNeedToken()
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
            BaseAuthorizeAttribute baseAuthorizeAttribute = new BaseAuthorizeAttribute(mockApplicationServicesFactory.Object) { dontNeedToken = true };

            UserAppModel userAppModel = new UserAppModel() { id = 55, active = true, username = "User" };

            mockSessionTokenService.Setup(x => x.DecryptToken(It.IsAny<string>(), It.IsAny<string>())).Returns(userAppModel);
            mockApplicationServicesFactory.Setup(x => x.CreateSessionTokenService()).Returns(mockSessionTokenService.Object);

            ActionContext actionContext = new ActionContext(mockContext.Object, new RouteData(), new ControllerActionDescriptor());

            #endregion

            #region === ACT ===

            AuthorizationFilterContext authorizationFilterContext = new AuthorizationFilterContext(actionContext, mockListIFilterMetadata.Object);
            baseAuthorizeAttribute.OnAuthorization(authorizationFilterContext);

            #endregion

            #region === ASSERT ===

            Assert.IsNotInstanceOfType(authorizationFilterContext.Result, typeof(UnauthorizedResult));

            #endregion
        }
    }
}
