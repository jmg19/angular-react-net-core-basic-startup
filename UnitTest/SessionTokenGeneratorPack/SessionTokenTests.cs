using Global.SessionTokenGeneratorPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Tests.SessionTokenGeneratorPack
{
    [TestClass]
    public class SessionTokenTests
    {
        [TestMethod]
        public void IsValid_Test() {
            #region === ARRANGE ===
            string UUID = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            
            SessionToken token1 = new SessionToken()
            {
                dateTime = DateTime.Now,
                UUID = UUID,
                userId = 55,
                userName = "User"
            };

            SessionToken token2 = new SessionToken()
            {
                dateTime = new DateTime(2000, 1, 1),                
                UUID = UUID,
                userId = 55,
                userName = "User"
            };

            SessionToken token3 = new SessionToken()
            {
                dateTime = new DateTime(2000, 1,1),
                daysToExpire = 0,
                UUID = UUID,
                userId = 55,
                userName = "User"
            };

            #endregion

            #region === ACT ===

            bool result1 = token1.IsValid(UUID);
            bool result2 = token1.IsValid("Some Other UUID");
            bool result3 = token2.IsValid(UUID);
            bool result4 = token3.IsValid(UUID);

            #endregion

            #region === ASSERT ===

            Assert.AreEqual<bool>(true, result1);
            Assert.AreEqual<bool>(false, result2);
            Assert.AreEqual<bool>(false, result3);
            Assert.AreEqual<bool>(true, result4);

            #endregion
        }
    }
}
