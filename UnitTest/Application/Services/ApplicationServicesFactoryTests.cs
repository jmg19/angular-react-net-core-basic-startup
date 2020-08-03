using BaseStartupProject.Application.Services;
using BaseStartupProject.Application.Services.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Tests.Application.Services
{
    [TestClass]
    public class ApplicationServicesFactoryTests
    {
        [TestMethod]
        public void CreateUserCommandsService_Test()
        {
            #region === ARRANGE ===

            ApplicationServicesFactory servicesFactory = new ApplicationServicesFactory();

            #endregion

            #region === ACT ===

            IUserCommandsService userCommandService = servicesFactory.CreateUserCommandsService();

            #endregion

            #region === ASSERT ===

            Assert.IsInstanceOfType(userCommandService, typeof(IUserCommandsService));

            #endregion
        }

        [TestMethod]
        public void CreateUserQueriesService_Test()
        {
            #region === ARRANGE ===

            ApplicationServicesFactory servicesFactory = new ApplicationServicesFactory();

            #endregion

            #region === ACT ===

            IUserQueriesService userQueriesService = servicesFactory.CreateUserQueriesService();

            #endregion

            #region === ASSERT ===

            Assert.IsInstanceOfType(userQueriesService, typeof(IUserQueriesService));

            #endregion
        }

        [TestMethod]
        public void CreateSessionTokenService_Test()
        {
            Assert.Inconclusive();

            #region === ARRANGE ===

            ApplicationServicesFactory servicesFactory = new ApplicationServicesFactory();

            #endregion

            #region === ACT ===

            ISessionTokenService sessionTokenService = servicesFactory.CreateSessionTokenService();

            #endregion

            #region === ASSERT ===

            Assert.IsInstanceOfType(sessionTokenService, typeof(ISessionTokenService));

            #endregion
        }
    }
}
