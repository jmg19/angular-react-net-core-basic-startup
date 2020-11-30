using BaseStartupProject.Application.AppModels.ResultModels;
using BaseStartupProject.Application.Mappers;
using BaseStartupProject.Infrastructure.Repositories.DataTableObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Tests.Application.Mappers
{
    [TestClass]
    public class MapperFactoryTests
    {
        [TestMethod]
        public void Create_DtoUser_To_UserAppModel_Mappert_Test() {
            #region === ARRANGE ===

            MapperFactory mapperFactory = new MapperFactory();

            #endregion

            #region === ACT ===

            var mapper = mapperFactory.Create<DtoUser, UserAppModel>();

            #endregion

            #region === ASSERT ===

            Assert.IsInstanceOfType(mapper, typeof(AbstractMapper<DtoUser, UserAppModel>));

            #endregion
        }
    }
}
