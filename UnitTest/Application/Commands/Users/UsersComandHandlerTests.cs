using BaseStartupProject.Application;
using BaseStartupProject.Application.InfrastructureOperations;
using BaseStartupProject.Application.Commands.Users;
using BaseStartupProject.Infrastructure.Repositories;
using BaseStartupProject.Infrastructure.Repositories.DataTableObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using BaseStartupProject.Business.Events;
using BaseStartupProject.Business;
using System.Runtime.InteropServices;

namespace BaseStartupProject.Tests.Application.Commands.Users
{
    [TestClass]
    public class UsersComandHandlerTests
    {
        [TestMethod]
        public void Handle_SignUpUser_Test() {
            #region === ARRANGE ===

            string expectedNewUserName = "New User";

            int businessChancesNumber = 0;
            int businessConsultNumber = 0;

            Mock<IMediatorsFactory> mockedBusinessEventsFactory = new Mock<IMediatorsFactory>();
            Mock<IInfrastructureOperationsFactory> mockedInfrastructureOperationsFactory = new Mock<IInfrastructureOperationsFactory>();
            BusinessDataMediator businessDataMediator = new BusinessDataMediator();

            businessDataMediator.AppendBusinessChangeNeedHandler((sender, args) => {
                businessChancesNumber++;
            });

            businessDataMediator.AppendBusinessConsultNeedHandler((sender, args) => {
                args.result = new List<User>();
                businessConsultNumber++;
            });

            mockedBusinessEventsFactory.Setup(x => x.CreateBusinessDataMediator(It.IsAny<EventHandler<BusinessConsultEventArgs>>(), It.IsAny<EventHandler<BusinessChangeEventArgs>>())).Returns(businessDataMediator);

            SignUpUser comand = new SignUpUser { Id = new Guid(), UserName = expectedNewUserName, Password = "New Pasword" };
            UsersComandHandler comandHandler = new UsersComandHandler(mockedBusinessEventsFactory.Object, mockedInfrastructureOperationsFactory.Object);

            #endregion

            #region === ACT ===

            comandHandler.Handle(comand);

            #endregion

            #region === ASSERT ===

            Assert.AreEqual<int>(1, businessConsultNumber);
            Assert.AreEqual<int>(2, businessChancesNumber);           

            #endregion
        }

        [TestMethod]
        public void Handle_UserLogin_Test()
        {
            #region === ARRANGE ===            
            
            int businessConsultNumber = 0;

            int id = 555;
            string username = "era uma vez . . . ";
            string password = "isto é um segredo . . . ";
            string hash = "223cb19e27ef605dcef5f03d977bf3c6cdc5161d489800117bddebdc2b0d08661ff5add09c812a3fdb9f5d98c737480994646f28cd63739fe565455c79b7e6ca";

            Mock<IMediatorsFactory> mockedBusinessEventsFactory = new Mock<IMediatorsFactory>();
            Mock<IMediatorsFactory> mockedBusinessEventsFactoryForNoUserFound = new Mock<IMediatorsFactory>();
            Mock<IInfrastructureOperationsFactory> mockedInfrastructureOperationsFactory = new Mock<IInfrastructureOperationsFactory>();
            BusinessDataMediator businessDataMediator = new BusinessDataMediator();
            BusinessDataMediator businessDataMediatorForNoUserFound = new BusinessDataMediator();
            User user = new User(id, username, hash, true, businessDataMediator); 

            businessDataMediator.AppendBusinessConsultNeedHandler((sender, args) => {
                args.result = new List<User>() { user };
                businessConsultNumber++;
            });

            businessDataMediatorForNoUserFound.AppendBusinessConsultNeedHandler((sender, args) => {
                args.result = new List<User>();
                businessConsultNumber++;
            });

            mockedBusinessEventsFactory.Setup(x => x.CreateBusinessDataMediator(It.IsAny<EventHandler<BusinessConsultEventArgs>>(), It.IsAny<EventHandler<BusinessChangeEventArgs>>())).Returns(businessDataMediator);
            mockedBusinessEventsFactoryForNoUserFound.Setup(x => x.CreateBusinessDataMediator(It.IsAny<EventHandler<BusinessConsultEventArgs>>(), It.IsAny<EventHandler<BusinessChangeEventArgs>>())).Returns(businessDataMediatorForNoUserFound);

            UserLogin command1 = new UserLogin { Id = new Guid(), UserName = username, Password = password };
            UserLogin command2 = new UserLogin { Id = new Guid(), UserName = username, Password = "Wrong Password" };
            UserLogin command3 = new UserLogin { Id = new Guid(), UserName = username, Password = password };
            UsersComandHandler comandHandler = new UsersComandHandler(mockedBusinessEventsFactory.Object, mockedInfrastructureOperationsFactory.Object);
            UsersComandHandler comandHandlerForNoUserFound = new UsersComandHandler(mockedBusinessEventsFactoryForNoUserFound.Object, mockedInfrastructureOperationsFactory.Object);

            #endregion

            #region === ACT ===

            comandHandler.Handle(command1);
            comandHandler.Handle(command2);
            comandHandlerForNoUserFound.Handle(command3);

            #endregion

            #region === ASSERT ===

            Assert.AreEqual<int>(3, businessConsultNumber);
            Assert.AreEqual<int>(id, command1.CommandResult);
            Assert.AreEqual<int>(0, command2.CommandResult);
            Assert.AreEqual<int>(0, command3.CommandResult);

            #endregion
        }

        [TestMethod]
        public void Handle_Activate_Test()
        {
            #region === ARRANGE ===            

            int businessConsultNumber = 0;
            int businessChangeNumber = 0;

            int id = 555;
            string username = "era uma vez . . . ";            
            string hash = "223cb19e27ef605dcef5f03d977bf3c6cdc5161d489800117bddebdc2b0d08661ff5add09c812a3fdb9f5d98c737480994646f28cd63739fe565455c79b7e6ca";

            Mock<IMediatorsFactory> mockedBusinessEventsFactory = new Mock<IMediatorsFactory>();            
            Mock<IInfrastructureOperationsFactory> mockedInfrastructureOperationsFactory = new Mock<IInfrastructureOperationsFactory>();
            BusinessDataMediator businessDataMediator = new BusinessDataMediator();           
            User user = new User(id, username, hash, false, businessDataMediator);

            businessDataMediator.AppendBusinessConsultNeedHandler((sender, args) => {
                args.result = new List<User>() { user };
                businessConsultNumber++;
            });

            businessDataMediator.AppendBusinessChangeNeedHandler((sender, handler) => { businessChangeNumber++; });

            mockedBusinessEventsFactory.Setup(x => x.CreateBusinessDataMediator(It.IsAny<EventHandler<BusinessConsultEventArgs>>(), It.IsAny<EventHandler<BusinessChangeEventArgs>>())).Returns(businessDataMediator);            

            ActivateUser command = new ActivateUser { Id = new Guid(), userId = id };
            UsersComandHandler comandHandler = new UsersComandHandler(mockedBusinessEventsFactory.Object, mockedInfrastructureOperationsFactory.Object);            

            #endregion

            #region === ACT ===

            comandHandler.Handle(command);


            #endregion

            #region === ASSERT ===

            Assert.AreEqual<int>(1, businessConsultNumber);
            Assert.AreEqual<int>(1, businessChangeNumber);
            Assert.AreEqual<bool>(true, user.active);            

            #endregion
        }

        [TestMethod]
        public void Handle_Inactivate_Test()
        {
            #region === ARRANGE ===            

            int businessConsultNumber = 0;
            int businessChangeNumber = 0;

            int id = 555;
            string username = "era uma vez . . . ";
            string hash = "223cb19e27ef605dcef5f03d977bf3c6cdc5161d489800117bddebdc2b0d08661ff5add09c812a3fdb9f5d98c737480994646f28cd63739fe565455c79b7e6ca";

            Mock<IMediatorsFactory> mockedBusinessEventsFactory = new Mock<IMediatorsFactory>();
            Mock<IInfrastructureOperationsFactory> mockedInfrastructureOperationsFactory = new Mock<IInfrastructureOperationsFactory>();
            BusinessDataMediator businessDataMediator = new BusinessDataMediator();
            User user = new User(id, username, hash, true, businessDataMediator);

            businessDataMediator.AppendBusinessConsultNeedHandler((sender, args) => {
                args.result = new List<User>() { user };
                businessConsultNumber++;
            });

            businessDataMediator.AppendBusinessChangeNeedHandler((sender, handler) => { businessChangeNumber++; });

            mockedBusinessEventsFactory.Setup(x => x.CreateBusinessDataMediator(It.IsAny<EventHandler<BusinessConsultEventArgs>>(), It.IsAny<EventHandler<BusinessChangeEventArgs>>())).Returns(businessDataMediator);

            InactivateUser command = new InactivateUser { Id = new Guid(), userId = id };
            UsersComandHandler comandHandler = new UsersComandHandler(mockedBusinessEventsFactory.Object, mockedInfrastructureOperationsFactory.Object);

            #endregion

            #region === ACT ===

            comandHandler.Handle(command);


            #endregion

            #region === ASSERT ===

            Assert.AreEqual<int>(1, businessConsultNumber);
            Assert.AreEqual<int>(1, businessChangeNumber);
            Assert.AreEqual<bool>(false, user.active);

            #endregion
        }

    }
}
