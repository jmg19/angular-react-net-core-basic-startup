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
    public class UsersRepositoryTests
    {
        [TestMethod]
        public void Get_OrderedAndBySearchRules_Test() {

            Mock<BaseStartupDemo> mockedContext = new Mock<BaseStartupDemo>();
            mockedContext.Object.Users = LoadMockedUsers();

            int expectedSize = 5;
            List<DtoUser> expectedResult1 = (from u in mockedContext.Object.Users where u.Active == true orderby u.Hash, u.ID descending select u).Skip(expectedSize).Take(expectedSize).ToList();
            List<DtoUser> expectedResult2 = (from u in mockedContext.Object.Users 
                                             where u.Active == true 
                                             && (u.ID < 20 || u.ID == 25)
                                             orderby u.Hash, u.ID descending select u).Skip(expectedSize).Take(expectedSize).ToList();

            UsersRepository repo = new UsersRepository(mockedContext.Object, true);
            List<SearchRule> rules1 = new List<SearchRule>();
            rules1.Add(new SearchRule { paging_rule = new PagingRule(), condition = "Active == true" });            
            rules1.Add(new SearchRule { paging_rule = new PagingRule() { page = 2, page_size = expectedSize } });

            List<SearchRule> rules2 = new List<SearchRule>();
            rules2.Add(new SearchRule { paging_rule = new PagingRule(), condition = "Active == true" });
            rules2.Add(new SearchRule { paging_rule = new PagingRule(), condition = "ID < 20 || ID == 25" });
            rules2.Add(new SearchRule { paging_rule = new PagingRule() { page = 2, page_size = expectedSize } });

            List<OrderingRule> orderingRules = new List<OrderingRule>();
            orderingRules.Add(new OrderingRule { fieldName = "Hash" });
            orderingRules.Add(new OrderingRule { fieldName = "ID", isDescending = true });

            var result1 = repo.Get(rules1.ToArray(), orderingRules.ToArray()).ToArray();
            var result2 = repo.Get(rules2.ToArray(), orderingRules.ToArray()).ToArray();

            Assert.AreEqual<int>(expectedSize, result1.Count());
            Assert.AreEqual<int>(expectedSize, result2.Count());

            for (int i = 0; i < expectedSize; i++) {
                Assert.AreEqual<int>(expectedResult1[i].ID, result1[i].ID);
                Assert.AreEqual<int>(expectedResult2[i].ID, result2[i].ID);
            }

        }

        [TestMethod]
        public void Get_All_Test()
        {
            Mock<BaseStartupDemo> mockedContext = new Mock<BaseStartupDemo>();
            mockedContext.Object.Users = LoadMockedUsers();            

            UsersRepository repo = new UsersRepository(mockedContext.Object, true);

            var result = repo.GetAll();

            Assert.AreEqual<int>(mockedContext.Object.Users.Count(), result.Count());            
        }

        [TestMethod]
        public void Get_ById_Test()
        {
            Mock<BaseStartupDemo> mockedContext = new Mock<BaseStartupDemo>();
            mockedContext.Object.Users = LoadMockedUsers();

            UsersRepository repo = new UsersRepository(mockedContext.Object, true);

            var result = repo.Get(10);

            Assert.AreEqual<int>(10, result.ID);
        }

        [TestMethod]
        public void Add_Test()
        {
            Mock<BaseStartupDemo> mockedContext = new Mock<BaseStartupDemo>();
            mockedContext.Object.Users = LoadMockedUsers();

            UsersRepository repo = new UsersRepository(mockedContext.Object, false);

            repo.Add(new DtoUser (0, "NewUser", "New User Hash", true));

            DtoUser added = (from u in mockedContext.Object.Users where u.UserName == "NewUser" select u).FirstOrDefault();

            Assert.IsNotNull(added);
        }

        [TestMethod]
        public void Update_Test()
        {
            Mock<BaseStartupDemo> mockedContext = new Mock<BaseStartupDemo>();
            mockedContext.Object.Users = LoadMockedUsers();

            UsersRepository repo = new UsersRepository(mockedContext.Object, false);

            repo.Update(new DtoUser(15, "UpdatedUser", "Updated User Hash", true));

            DtoUser updated = (from u in mockedContext.Object.Users where u.ID == 15 select u).FirstOrDefault();

            Assert.AreEqual<string>("UpdatedUser", updated.UserName);
            Assert.AreEqual<string>("Updated User Hash", updated.Hash);
            Assert.AreEqual<bool>(true, updated.Active);
        }

        [TestMethod]
        public void Delete_Test()
        {
            Mock<BaseStartupDemo> mockedContext = new Mock<BaseStartupDemo>();
            mockedContext.Object.Users = LoadMockedUsers();

            UsersRepository repo = new UsersRepository(mockedContext.Object, false);

            repo.Delete(15);

            DtoUser deleted = (from u in mockedContext.Object.Users where u.ID == 15 select u).FirstOrDefault();

            Assert.IsNull(deleted);
        }

        [TestMethod]
        public void Add_In_ReadOnly_Test()
        {
            Mock<BaseStartupDemo> mockedContext = new Mock<BaseStartupDemo>();
            mockedContext.Object.Users = LoadMockedUsers();

            UsersRepository repo = new UsersRepository(mockedContext.Object, true);
            Exception expected_ex = null;
            try
            {
                repo.Add(new DtoUser(0, "NewUser", "New User Hash", true));
            }
            catch(ReadOnlyException ex) { expected_ex = ex; }

            Assert.IsNotNull(expected_ex);
        }

        [TestMethod]
        public void Update_In_ReadOnly_Test()
        {
            Mock<BaseStartupDemo> mockedContext = new Mock<BaseStartupDemo>();
            mockedContext.Object.Users = LoadMockedUsers();

            UsersRepository repo = new UsersRepository(mockedContext.Object, true);
            Exception expected_ex = null;
            try
            {
                repo.Update(new DtoUser(15, "UpdatedUser", "Updated User Hash", true));
            }
            catch (ReadOnlyException ex) { expected_ex = ex; }

            Assert.IsNotNull(expected_ex);
        }

        [TestMethod]
        public void Delete_In_ReadOnly_Test()
        {
            Mock<BaseStartupDemo> mockedContext = new Mock<BaseStartupDemo>();
            mockedContext.Object.Users = LoadMockedUsers();

            UsersRepository repo = new UsersRepository(mockedContext.Object, true);
            Exception expected_ex = null;
            try
            {
                repo.Delete(15);
            }
            catch (ReadOnlyException ex) { expected_ex = ex; }

            Assert.IsNotNull(expected_ex);            
        }

        private DbSet<DtoUser> LoadMockedUsers()
        {
            List<DtoUser> users = new List<DtoUser>();
            users.Add(new DtoUser(1, "User1", "ASomeHash", true));
            users.Add(new DtoUser(2, "User2", "ASomeHash", true));
            users.Add(new DtoUser(3, "User3", "BSomeHash", true));
            users.Add(new DtoUser(4, "User4", "SomeHash", false));
            users.Add(new DtoUser(5, "User5", "SomeHash", true));
            users.Add(new DtoUser(6, "User6", "SomeHash", true));
            users.Add(new DtoUser(7, "User7", "ASomeHash", true));
            users.Add(new DtoUser(8, "User8", "BSomeHash", false));
            users.Add(new DtoUser(9, "User9", "BSomeHash", true));
            users.Add(new DtoUser(10, "User10", "SomeHash", true));
            users.Add(new DtoUser(11, "User11", "SomeHash", true));
            users.Add(new DtoUser(12, "User12", "ASomeHash", false));
            users.Add(new DtoUser(13, "User13", "ASomeHash", true));
            users.Add(new DtoUser(14, "User14", "SomeHash", true));
            users.Add(new DtoUser(15, "User15", "BSomeHash", true));
            users.Add(new DtoUser(16, "User16", "SomeHash", false));
            users.Add(new DtoUser(17, "User17", "BSomeHash", true));
            users.Add(new DtoUser(18, "User18", "SomeHash", true));
            users.Add(new DtoUser(19, "User19", "ASomeHash", true));
            users.Add(new DtoUser(20, "User20", "SomeHash", false));
            users.Add(new DtoUser(21, "User21", "SomeHash", true));
            users.Add(new DtoUser(22, "User22", "BSomeHash", true));
            users.Add(new DtoUser(23, "User23", "SomeHash", true));
            users.Add(new DtoUser(24, "User24", "BSomeHash", false));
            users.Add(new DtoUser(25, "User25", "BSomeHash", true));
            users.Add(new DtoUser(26, "User26", "SomeHash", true));
            users.Add(new DtoUser(27, "User27", "SomeHash", true));
            users.Add(new DtoUser(28, "User28", "SomeHash", false));
            users.Add(new DtoUser(29, "User29", "SomeHash", true));
            users.Add(new DtoUser(30, "User30", "SomeHash", true));
            users.Add(new DtoUser(31, "User31", "SomeHash", true));
            users.Add(new DtoUser(32, "User32", "SomeHash", false));
            users.Add(new DtoUser(33, "User33", "SomeHash", true));
            users.Add(new DtoUser(34, "User34", "SomeHash", true));
            users.Add(new DtoUser(35, "User35", "SomeHash", true));
            users.Add(new DtoUser(36, "User36", "SomeHash", false));
            users.Add(new DtoUser(37, "User37", "SomeHash", true));
            users.Add(new DtoUser(38, "User38", "SomeHash", true));
            users.Add(new DtoUser(39, "User39", "SomeHash", true));
            users.Add(new DtoUser(40, "User40", "SomeHash", false));

            return MockHelpers.GetQueryableMockDbSet<DtoUser>(users);
        }
    }
}
