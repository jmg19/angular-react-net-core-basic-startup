using BaseStartupProject.Business.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseStartupProject.Tests.Business.Utils
{
    [TestClass]
    public class LoginPasswordHasherTests
    {
        [TestMethod]
        public void GenerateUserHash_Test()
        {
            int id = 555;
            string username = "era uma vez . . . ";
            string password = "isto é um segredo . . . ";
            string espected_hash = "223cb19e27ef605dcef5f03d977bf3c6cdc5161d489800117bddebdc2b0d08661ff5add09c812a3fdb9f5d98c737480994646f28cd63739fe565455c79b7e6ca";

            LoginPasswordHasher hasher = new LoginPasswordHasher();
            string hash = hasher.GenerateUserHash(id, username, password);

            Assert.AreEqual<string>(espected_hash, hash);
        }
    }
}
