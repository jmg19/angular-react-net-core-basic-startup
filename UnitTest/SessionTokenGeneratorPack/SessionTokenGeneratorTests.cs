using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Global.SessionTokenGeneratorPack;

namespace BaseStartupProject.Tests.SessionTokenGeneratorPack
{
    [TestClass]
    public class SessionTokenGeneratorTests
    {
        [TestMethod]
        public void Encript_Test() {
            #region === ARRANGE ===

            SessionTokenGenerator sessionTokenGenerator = new SessionTokenGenerator("Some Key!");
            SessionToken token = new SessionToken()
            {
                dateTime = new DateTime(2020, 1, 1),
                UUID = "Some UUID",
                userId = 55,
                userName = "User"
            };

            string expectedToken = "AcTw4K2/YZZ34viuf5skzVmPweTE9qQt7JvEJTTBCd6hXBrI4X/GhRey6TcpjlyjEPoYmz7ABtnIzqwRuCMvkg==";

            #endregion

            #region === ACT ===

            string tokenString = sessionTokenGenerator.Encript(token);

            #endregion

            #region === ASSERT ===

            Assert.AreEqual<string>(expectedToken, tokenString);

            #endregion
        }

        [TestMethod]
        public void Decript_Test()
        {
            #region === ARRANGE ===

            SessionTokenGenerator sessionTokenGenerator = new SessionTokenGenerator("Some Key!");
            SessionToken expectedToken = new SessionToken()
            {
                dateTime = new DateTime(2020, 1, 1),
                UUID = "Some UUID",
                userId = 55,
                userName = "User"
            };

            string tokenString = "AcTw4K2/YZZ34viuf5skzVmPweTE9qQt7JvEJTTBCd6hXBrI4X/GhRey6TcpjlyjEPoYmz7ABtnIzqwRuCMvkg==";

            #endregion

            #region === ACT ===

            SessionToken result = sessionTokenGenerator.Decrypt(tokenString);

            #endregion

            #region === ASSERT ===

            Assert.AreEqual<DateTime>(expectedToken.dateTime, result.dateTime);
            Assert.AreEqual<int>(expectedToken.daysToExpire, result.daysToExpire);
            Assert.AreEqual<string>(expectedToken.UUID, result.UUID);
            Assert.AreEqual<int>(expectedToken.userId, result.userId);
            Assert.AreEqual<string>(expectedToken.userName, result.userName);

            #endregion
        }
    }
}
