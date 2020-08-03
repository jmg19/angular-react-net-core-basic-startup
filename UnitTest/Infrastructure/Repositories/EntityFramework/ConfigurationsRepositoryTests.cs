using BaseStartupProject.Infrastructure.Repositories.DataTableObjects;
using BaseStartupProject.Infrastructure.Repositories.EntityFramework;
using BaseStartupProject.Infrastructure.Repositories.RepositoryExceptions;
using BaseStartupProject.Infrastructure.Repositories.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseStartupProject.Tests.Infrastructure.Repositories.EntityFramework
{
    [TestClass]
    public class ConfigurationsRepositoryTests
    {        
        [TestMethod]
        public void Get_All_Test()
        {
            Mock<BaseStartupDemo> mockedContext = new Mock<BaseStartupDemo>();
            mockedContext.Object.Configurations = LoadMockedUsers();            

            ConfigurationsRepository repo = new ConfigurationsRepository(mockedContext.Object, true);

            var result = repo.GetAll();

            Assert.AreEqual<int>(mockedContext.Object.Configurations.Count(), result.Count());            
        }

        [TestMethod]
        public void Get_ById_Test()
        {
            Mock<BaseStartupDemo> mockedContext = new Mock<BaseStartupDemo>();
            mockedContext.Object.Configurations = LoadMockedUsers();

            ConfigurationsRepository repo = new ConfigurationsRepository(mockedContext.Object, true);

            var result = repo.Get(1);

            Assert.AreEqual<int>(1, result.ID);
        }

        [TestMethod]
        public void Add_Test()
        {
            Mock<BaseStartupDemo> mockedContext = new Mock<BaseStartupDemo>();
            mockedContext.Object.Configurations = LoadMockedUsers();

            ConfigurationsRepository repo = new ConfigurationsRepository(mockedContext.Object, false);

            repo.Add(new DtoConfiguration (0, "New Config", "New Value"));

            DtoConfiguration added = (from u in mockedContext.Object.Configurations where u.Name == "New Config" select u).FirstOrDefault();

            Assert.IsNotNull(added);
        }

        [TestMethod]
        public void Update_Test()
        {
            Mock<BaseStartupDemo> mockedContext = new Mock<BaseStartupDemo>();
            mockedContext.Object.Configurations = LoadMockedUsers();

            ConfigurationsRepository repo = new ConfigurationsRepository(mockedContext.Object, false);

            repo.Update(new DtoConfiguration(1, "Updated Config", "Updated Config Value"));

            DtoConfiguration updated = (from u in mockedContext.Object.Configurations where u.ID == 1 select u).FirstOrDefault();

            Assert.AreEqual<string>("Updated Config", updated.Name);
            Assert.AreEqual<string>("Updated Config Value", updated.Value);
        }

        [TestMethod]
        public void Delete_Test()
        {
            Mock<BaseStartupDemo> mockedContext = new Mock<BaseStartupDemo>();
            mockedContext.Object.Configurations = LoadMockedUsers();

            ConfigurationsRepository repo = new ConfigurationsRepository(mockedContext.Object, false);

            repo.Delete(1);

            DtoConfiguration deleted = (from u in mockedContext.Object.Configurations where u.ID == 1 select u).FirstOrDefault();

            Assert.IsNull(deleted);
        }

        [TestMethod]
        public void Add_In_ReadOnly_Test()
        {
            Mock<BaseStartupDemo> mockedContext = new Mock<BaseStartupDemo>();
            mockedContext.Object.Configurations = LoadMockedUsers();

            ConfigurationsRepository repo = new ConfigurationsRepository(mockedContext.Object, true);
            Exception expected_ex = null;
            try
            {
                repo.Add(new DtoConfiguration(0, "New Config", "New Value"));
            }
            catch(ReadOnlyException ex) { expected_ex = ex; }

            Assert.IsNotNull(expected_ex);
        }

        [TestMethod]
        public void Update_In_ReadOnly_Test()
        {
            Mock<BaseStartupDemo> mockedContext = new Mock<BaseStartupDemo>();
            mockedContext.Object.Configurations = LoadMockedUsers();

            ConfigurationsRepository repo = new ConfigurationsRepository(mockedContext.Object, true);
            Exception expected_ex = null;
            try
            {
                repo.Update(new DtoConfiguration(1, "Updated Config", "Updated Config Value"));
            }
            catch (ReadOnlyException ex) { expected_ex = ex; }

            Assert.IsNotNull(expected_ex);
        }

        [TestMethod]
        public void Delete_In_ReadOnly_Test()
        {
            Mock<BaseStartupDemo> mockedContext = new Mock<BaseStartupDemo>();
            mockedContext.Object.Configurations = LoadMockedUsers();

            ConfigurationsRepository repo = new ConfigurationsRepository(mockedContext.Object, true);
            Exception expected_ex = null;
            try
            {
                repo.Delete(15);
            }
            catch (ReadOnlyException ex) { expected_ex = ex; }

            Assert.IsNotNull(expected_ex);            
        }

        private DbSet<DtoConfiguration> LoadMockedUsers()
        {
            List<DtoConfiguration> configurations = new List<DtoConfiguration>();
            configurations.Add(new DtoConfiguration(1, "conf1", "confValue1"));

            return MockHelpers.GetQueryableMockDbSet<DtoConfiguration>(configurations);
        }
    }
}
