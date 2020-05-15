using IntimateDesires.Infrastructure.Repositories.DataTableObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntimateDesires.Infrastructure.Tests
{
    [TestClass]
    public class InfrastructureConfigurationLoaderTest
    {
        [TestMethod]
        public void Load_Test()
        {
            InfrastructureConfigurationLoader loader = new InfrastructureConfigurationLoader();
            var settings = loader.Load("InfrastructureConfigurationLoaderTest.json");

            Assert.AreEqual<string>("Test Connection", settings.sql_Connection);

        }
    }
}
