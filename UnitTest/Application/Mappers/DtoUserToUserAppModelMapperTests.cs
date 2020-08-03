using BaseStartupProject.Application.AppModels.ResultModels;
using BaseStartupProject.Application.Mappers;
using BaseStartupProject.Business;
using BaseStartupProject.Infrastructure.Repositories.DataTableObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Tests.Application.Mappers
{
    [TestClass]
    public class DtoUserToUserAppModelMapperTests
    {
        [TestMethod]
        public void Map_Test()
        {
            #region === ARRANGE ===

            DtoUser user = new DtoUser(777, "xpto", "hash", true);
            DtoUserToUserAppModelMapper mapper = new DtoUserToUserAppModelMapper();

            #endregion

            #region === ACT ===

            UserAppModel result = mapper.Map(user);

            #endregion

            #region === ASSERT ===

            Assert.AreEqual<int>(user.ID, result.id);
            Assert.AreEqual<string>(user.UserName, result.username);
            Assert.AreEqual<bool>(user.Active, result.active);

            #endregion
        }

        [TestMethod]
        public void MapList_Test()
        {
            #region === ARRANGE ===

            List<DtoUser> userList = new List<DtoUser>()
            {
                new DtoUser(771, "xpto1", "hash", true),
                new DtoUser(772, "xpto2", "hash", false),
                new DtoUser(773, "xpto3", "hash", true),
                new DtoUser(774, "xpto4", "hash", false),
                new DtoUser(775, "xpto5", "hash", true),
                new DtoUser(776, "xpto6", "hash", false),
                new DtoUser(777, "xpto7", "hash", true),
            };

            DtoUserToUserAppModelMapper mapper = new DtoUserToUserAppModelMapper();

            #endregion

            #region === ACT ===

            IList<UserAppModel> result = mapper.Map(userList);

            #endregion

            #region === ASSERT ===

            Assert.AreEqual<int>(userList.Count, result.Count);

            for (int i = 0; i < userList.Count; i++) { 
                Assert.AreEqual<int>(userList[i].ID, result[i].id);
                Assert.AreEqual<string>(userList[i].UserName, result[i].username);
                Assert.AreEqual<bool>(userList[i].Active, result[i].active);            
            }

            #endregion
        }
    }
}
