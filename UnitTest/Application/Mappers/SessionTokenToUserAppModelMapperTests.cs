using BaseStartupProject.Application.AppModels.ResultModels;
using BaseStartupProject.Application.Mappers;
using Global.SessionTokenGeneratorPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Tests.Application.Mappers
{
    [TestClass]
    public class SessionTokenToUserAppModelMapperTests
    {
        [TestMethod]
        public void Map_Test()
        {
            #region === ARRANGE ===

            SessionToken token = new SessionToken
            {
                userId = 555,
                userName = "Some User"
            };

            SessionTokenToUserAppModelMapper mapper = new SessionTokenToUserAppModelMapper();

            #endregion

            #region === ACT ===

            UserAppModel result = mapper.Map(token);

            #endregion

            #region === ASSERT ===

            Assert.AreEqual<int>(token.userId, result.id);
            Assert.AreEqual<string>(token.userName, result.username);
            Assert.AreEqual<bool>(true, result.active);

            #endregion
        }

        [TestMethod]
        public void MapList_Test()
        {
            #region === ARRANGE ===

            List<SessionToken> tokenList = new List<SessionToken>()
            {
                new SessionToken{ userId = 771, userName = "xpto1" },
                new SessionToken{ userId = 772, userName = "xpto2" },
                new SessionToken{ userId = 773, userName = "xpto3" },
                new SessionToken{ userId = 774, userName = "xpto4" },
                new SessionToken{ userId = 775, userName = "xpto5" },
                new SessionToken{ userId = 776, userName = "xpto6" },
                new SessionToken{ userId = 777, userName = "xpto7" }
            };

            SessionTokenToUserAppModelMapper mapper = new SessionTokenToUserAppModelMapper();

            #endregion

            #region === ACT ===

            IList<UserAppModel> result = mapper.Map(tokenList);

            #endregion

            #region === ASSERT ===

            Assert.AreEqual<int>(tokenList.Count, result.Count);

            for (int i = 0; i < tokenList.Count; i++)
            {
                Assert.AreEqual<int>(tokenList[i].userId, result[i].id);
                Assert.AreEqual<string>(tokenList[i].userName, result[i].username);
                Assert.AreEqual<bool>(true, result[i].active);
            }

            #endregion
        }
    }
}
