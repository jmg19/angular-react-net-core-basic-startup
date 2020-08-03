using BaseStartupProject.Application.Commands;
using BaseStartupProject.Application.Commands.Users;
using BaseStartupProject.Application.Services.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Tests.Application.Services
{
    [TestClass]
    public class UserCommandsServiceTests
    {
        [TestMethod]
        public void Login_Test() {
            #region === ARRANGE ===

            Mock<ICommandHandler<UserLogin>> mockCommandHandler = new Mock<ICommandHandler<UserLogin>>();
            Mock<ICommandsHandleresFactory> mockCommandHandlerFactory = new Mock<ICommandsHandleresFactory>();
            mockCommandHandlerFactory.Setup(x => x.CreateCommandHandler<UserLogin>()).Returns(mockCommandHandler.Object);
            UserCommandsService userCommandsService = new UserCommandsService(mockCommandHandlerFactory.Object);

            #endregion

            #region === ACT ===

            userCommandsService.Login(new UserLogin());

            #endregion

            #region === ASSERT ===

            mockCommandHandler.Verify(x => x.Handle(It.IsAny<UserLogin>()), Times.Once);

            #endregion
        }

        [TestMethod]
        public void SignUpNewUser_Test()
        {
            #region === ARRANGE ===

            Mock<ICommandHandler<SignUpUser>> mockCommandHandler = new Mock<ICommandHandler<SignUpUser>>();
            Mock<ICommandsHandleresFactory> mockCommandHandlerFactory = new Mock<ICommandsHandleresFactory>();
            mockCommandHandlerFactory.Setup(x => x.CreateCommandHandler<SignUpUser>()).Returns(mockCommandHandler.Object);
            UserCommandsService userCommandsService = new UserCommandsService(mockCommandHandlerFactory.Object);

            #endregion

            #region === ACT ===

            userCommandsService.SignUpNewUser(new SignUpUser());

            #endregion

            #region === ASSERT ===

            mockCommandHandler.Verify(x => x.Handle(It.IsAny<SignUpUser>()), Times.Once);

            #endregion
        }

        [TestMethod]
        public void Activate_Test()
        {
            #region === ARRANGE ===

            Mock<ICommandHandler<ActivateUser>> mockCommandHandler = new Mock<ICommandHandler<ActivateUser>>();
            Mock<ICommandsHandleresFactory> mockCommandHandlerFactory = new Mock<ICommandsHandleresFactory>();
            mockCommandHandlerFactory.Setup(x => x.CreateCommandHandler<ActivateUser>()).Returns(mockCommandHandler.Object);
            UserCommandsService userCommandsService = new UserCommandsService(mockCommandHandlerFactory.Object);

            #endregion

            #region === ACT ===

            userCommandsService.Activate(new ActivateUser());

            #endregion

            #region === ASSERT ===

            mockCommandHandler.Verify(x => x.Handle(It.IsAny<ActivateUser>()), Times.Once);

            #endregion
        }

        [TestMethod]
        public void Inactivate_Test()
        {
            #region === ARRANGE ===

            Mock<ICommandHandler<InactivateUser>> mockCommandHandler = new Mock<ICommandHandler<InactivateUser>>();
            Mock<ICommandsHandleresFactory> mockCommandHandlerFactory = new Mock<ICommandsHandleresFactory>();
            mockCommandHandlerFactory.Setup(x => x.CreateCommandHandler<InactivateUser>()).Returns(mockCommandHandler.Object);
            UserCommandsService userCommandsService = new UserCommandsService(mockCommandHandlerFactory.Object);

            #endregion

            #region === ACT ===

            userCommandsService.Inactivate(new InactivateUser());

            #endregion

            #region === ASSERT ===

            mockCommandHandler.Verify(x => x.Handle(It.IsAny<InactivateUser>()), Times.Once);

            #endregion
        }
    }
}
