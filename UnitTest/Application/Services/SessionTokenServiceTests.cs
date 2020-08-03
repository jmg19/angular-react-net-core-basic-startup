using BaseStartupProject.Application.AppModels.ResultModels;
using BaseStartupProject.Application.Exceptions;
using BaseStartupProject.Application.Services.User;
using BaseStartupProject.Business;
using BaseStartupProject.Infrastructure.Repositories;
using BaseStartupProject.Infrastructure.Repositories.DataTableObjects;
using Global.SessionTokenGeneratorPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Tests.Application.Services
{
    [TestClass]
    public class SessionTokenServiceTests
    {
        [TestMethod]
        public void Create_SessionTokenServiceTests_Test()
        {
            #region === ARRANGE ===

            Mock<IRepositoryFactory> mockRepositoryFactory1 = new Mock<IRepositoryFactory>();            
            Mock<IReadOnlyRepository<DtoConfiguration>> mockConfigurationRepository1 = new Mock<IReadOnlyRepository<DtoConfiguration>>();
            Mock<IRepositoryFactory> mockRepositoryFactory2 = new Mock<IRepositoryFactory>();
            Mock<IReadOnlyRepository<DtoConfiguration>> mockConfigurationRepository2 = new Mock<IReadOnlyRepository<DtoConfiguration>>();

            mockRepositoryFactory1.Setup(x => x.CreateReadOnlyConfigurationsRepository()).Returns(mockConfigurationRepository1.Object);
            mockRepositoryFactory2.Setup(x => x.CreateReadOnlyConfigurationsRepository()).Returns(mockConfigurationRepository2.Object);

            mockConfigurationRepository1.Setup(x => x.GetAll()).Returns(new DtoConfiguration[] { new DtoConfiguration(1, "tokenEncriptKey", "Bla Bla") });
            mockConfigurationRepository2.Setup(x => x.GetAll()).Returns(new DtoConfiguration[] { new DtoConfiguration(1, "Bla Bla", "Bla Bla") });


            #endregion

            #region === ACT ===

            Exception exception1 = null;
            Exception exception2 = null;

            try
            {
                SessionTokenService service = new SessionTokenService(mockRepositoryFactory1.Object);
            }catch(Exception ex)
            {
                exception1 = ex;
            }

            try
            {
                SessionTokenService service = new SessionTokenService(mockRepositoryFactory2.Object);
            }
            catch (Exception ex)
            {
                exception2 = ex;
            }

            #endregion

            #region === ASSERT ===

            Assert.IsNull(exception1);
            Assert.IsNotNull(exception2);
            Assert.IsInstanceOfType(exception2, typeof(SystemConfigurationMissingException));

            #endregion
        }

        [TestMethod]
        public void DecryptToken_Test()
        {
            #region === ARRANGE ===

            SessionToken expectedToken = new SessionToken
            {
                userId = 999,
                userName = "Olá",
                dateTime = DateTime.Now,
                daysToExpire = 0,
                UUID = "Some UUID"
            };

            Mock<IRepositoryFactory> mockRepositoryFactory = new Mock<IRepositoryFactory>();
            Mock<IReadOnlyRepository<DtoConfiguration>> mockConfigurationRepository = new Mock<IReadOnlyRepository<DtoConfiguration>>();
            Mock<ISessionTokenGenerator> mockSessionTokenGenerator = new Mock<ISessionTokenGenerator>();

            mockRepositoryFactory.Setup(x => x.CreateReadOnlyConfigurationsRepository()).Returns(mockConfigurationRepository.Object);
            mockConfigurationRepository.Setup(x => x.GetAll()).Returns(new DtoConfiguration[] { new DtoConfiguration(1, "tokenEncriptKey", "Bla Bla") });
            mockSessionTokenGenerator.Setup(x => x.Decrypt(It.IsAny<string>())).Returns(expectedToken);


            #endregion

            #region === ACT ===

            SessionTokenService service = new SessionTokenService(mockRepositoryFactory.Object, mockSessionTokenGenerator.Object);
            UserAppModel userAppModel = service.DecryptToken(expectedToken.UUID, "some token string");

            #endregion

            #region === ASSERT ===

            mockSessionTokenGenerator.Verify(x => x.Decrypt(It.IsAny<string>()), Times.Once);
            Assert.AreEqual<int>(userAppModel.id, expectedToken.userId);
            Assert.AreEqual<string>(userAppModel.username, expectedToken.userName);

            #endregion
        }

        [TestMethod]
        public void GenerateToken_Test()
        {
            #region === ARRANGE ===

            UserAppModel user = new UserAppModel
            {
                id = 999,
                username = "Olá",
                active = true
            };

            Mock<IRepositoryFactory> mockRepositoryFactory = new Mock<IRepositoryFactory>();
            Mock<IReadOnlyRepository<DtoConfiguration>> mockConfigurationRepository = new Mock<IReadOnlyRepository<DtoConfiguration>>();
            Mock<ISessionTokenGenerator> mockSessionTokenGenerator = new Mock<ISessionTokenGenerator>();

            mockRepositoryFactory.Setup(x => x.CreateReadOnlyConfigurationsRepository()).Returns(mockConfigurationRepository.Object);
            mockConfigurationRepository.Setup(x => x.GetAll()).Returns(new DtoConfiguration[] { new DtoConfiguration(1, "tokenEncriptKey", "Bla Bla") });
            mockSessionTokenGenerator.Setup(x => x.Encript(It.IsAny<SessionToken>())).Returns("Some Encripted Token");

            #endregion

            #region === ACT ===

            SessionTokenService service = new SessionTokenService(mockRepositoryFactory.Object, mockSessionTokenGenerator.Object);
            service.GenerateToken("Some UUID", user, false);

            #endregion

            #region === ASSERT ===            

            mockSessionTokenGenerator.Verify(x => x.Encript(It.IsAny<SessionToken>()), Times.Once);

            #endregion
        }
    }
}
