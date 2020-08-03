using BaseStartupProject.Application.Commands;
using BaseStartupProject.Application.Commands.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Tests.Application.Commands
{
    [TestClass]
    public class CommandHandlerFactory_Tests
    {
        [TestMethod]
        public void CreateCommandHandler_For_SignUpUserCommand_Test() {
            #region === ARRANGE ===

            Type expected = typeof(ICommandHandler<SignUpUser>);
            CommandsHandleresFactory factory = new CommandsHandleresFactory();

            #endregion

            #region === ACT ===

            object result = factory.CreateCommandHandler<SignUpUser>();

            #endregion

            #region === ASSERT ===

            Assert.IsInstanceOfType(result, expected);

            #endregion
        }

        [TestMethod]
        public void CreateCommandHandler_For_UserLoginCommand_Test()
        {
            #region === ARRANGE ===

            Type expected = typeof(ICommandHandler<UserLogin>);
            CommandsHandleresFactory factory = new CommandsHandleresFactory();

            #endregion

            #region === ACT ===

            object result = factory.CreateCommandHandler<UserLogin>();

            #endregion

            #region === ASSERT ===

            Assert.IsInstanceOfType(result, expected);

            #endregion
        }
    }
}
