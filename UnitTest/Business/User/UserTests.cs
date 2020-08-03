using BaseStartupProject.Business;
using BaseStartupProject.Business.Events;
using BaseStartupProject.Business.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;


namespace BaseStartupProject.Tests.Business.UserBusiness
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void GenerateUserHashDoLogin_WithValidPasswrod_Test()
        {
            int id = 555;
            string username = "era uma vez . . . ";
            string password = "isto é um segredo . . . ";
            string hash = "223cb19e27ef605dcef5f03d977bf3c6cdc5161d489800117bddebdc2b0d08661ff5add09c812a3fdb9f5d98c737480994646f28cd63739fe565455c79b7e6ca";

            BusinessDataMediator businessEvents = new BusinessDataMediator();
            User user = new User(id, username, hash, true, businessEvents);

            Assert.AreEqual<bool>(true, user.DoLogin(password));
        }

        [TestMethod]
        public void GenerateUserHashDoLogin_WithInvalidPasswrod_Test()
        {
            int id = 555;
            string username = "era uma vez . . . ";
            
            string hash = "223cb19e27ef605dcef5f03d977bf3c6cdc5161d489800117bddebdc2b0d08661ff5add09c812a3fdb9f5d98c737480994646f28cd63739fe565455c79b7e6ca";

            BusinessDataMediator businessEvents = new BusinessDataMediator();
            User user = new User(id, username, hash, true, businessEvents);

            Assert.AreEqual<bool>(false, user.DoLogin("Invalid!"));
        }

        [TestMethod]
        public void ChangePassword_Test()
        {
            bool updateTriggered = false;

            int id = 555;
            string username = "era uma vez . . . ";
            string password = "isto é um segredo . . . ";
            string hash = "223cb19e27ef605dcef5f03d977bf3c6cdc5161d489800117bddebdc2b0d08661ff5add09c812a3fdb9f5d98c737480994646f28cd63739fe565455c79b7e6ca";

            BusinessDataMediator businessEvents = new BusinessDataMediator();
            businessEvents.AppendBusinessChangeNeedHandler((sender, args) => { updateTriggered = true; });
            User user = new User(id, username, hash, true, businessEvents);

            Assert.AreEqual<bool>(true, user.DoLogin(password));

            user.SetPassword("new password");

            Assert.AreEqual<bool>(true, updateTriggered);
            Assert.AreEqual<bool>(false, user.DoLogin(password));
        }

        [TestMethod]
        public void Activate_Test()
        {
            bool updateTriggered = false;

            int id = 555;
            string username = "era uma vez . . . ";

            string hash = "223cb19e27ef605dcef5f03d977bf3c6cdc5161d489800117bddebdc2b0d08661ff5add09c812a3fdb9f5d98c737480994646f28cd63739fe565455c79b7e6ca";

            BusinessDataMediator businessEvents = new BusinessDataMediator();
            businessEvents.AppendBusinessChangeNeedHandler((sender, args) => { updateTriggered = true; });
            User user = new User(id, username, hash, false, businessEvents);            

            user.Activate();

            Assert.AreEqual<bool>(true, updateTriggered);
            Assert.AreEqual<bool>(true, user.active);
        }

        [TestMethod]
        public void Inactivate_Test()
        {            
            bool updateTriggered = false;
            int id = 555;
            string username = "era uma vez . . . ";

            string hash = "223cb19e27ef605dcef5f03d977bf3c6cdc5161d489800117bddebdc2b0d08661ff5add09c812a3fdb9f5d98c737480994646f28cd63739fe565455c79b7e6ca";

            BusinessDataMediator businessEvents = new BusinessDataMediator();
            businessEvents.AppendBusinessChangeNeedHandler((sender, args) => { updateTriggered = true; });
            User user = new User(id, username, hash, true, businessEvents);

            
            user.Inactivate();

            Assert.AreEqual<bool>(true, updateTriggered);
            Assert.AreEqual<bool>(false, user.active);
        }        
    }
}
