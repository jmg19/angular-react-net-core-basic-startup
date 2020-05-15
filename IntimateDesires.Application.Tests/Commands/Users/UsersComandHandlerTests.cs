using IntimateDesires.Application.InfrastructureOperationsDelegations;
using IntimateDesires.Application.Commands.Users;
using IntimateDesires.Infrastructure.Repositories;
using IntimateDesires.Infrastructure.Repositories.DataTableObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using IntimateDesires.Business.Events;

namespace IntimateDesires.Application.Tests.Commands.Users
{
    [TestClass]
    public class UsersComandHandlerTests
    {
        [TestMethod]
        public void Handle_SignUpUser_Test() {
            
            string expectedNewUserName = "New User";

            int businessChancesNumber = 0;
            int businessConsultNumber = 0;

            Mock<IBusinessEventsFactory> mockedBusinessEventsFactory = new Mock<IBusinessEventsFactory>();
            Mock<IInfrastructureOperationDelegator> mockedInfrastructureOperationDelegator = new Mock<IInfrastructureOperationDelegator>();
            IBusinessEvents businessEvents = new BusinessEvents();

            businessEvents.AppendBusinessChangeNeedHandler((sender, args) => {
                businessChancesNumber++;
            });

            businessEvents.AppendBusinessConsultNeedHandler((sender, args) => {
                args.entitiesListResult = new List<Business.BusinessObject>();
                businessConsultNumber++;
            });

            mockedBusinessEventsFactory.Setup(x => x.CreateBusinessEvents(It.IsAny<EventHandler<BusinessConsultEventArgs>>(), It.IsAny<EventHandler<BusinessChangeEventArgs>>())).Returns(businessEvents);

            SignUpUser comand = new SignUpUser { Id = new Guid(), UserName = expectedNewUserName, Password = "New Pasword" };
            UsersComandHandler comandHandler = new UsersComandHandler(mockedBusinessEventsFactory.Object, mockedInfrastructureOperationDelegator.Object);

            comandHandler.Handle(comand);

            Assert.AreEqual<int>(1, businessConsultNumber);
            Assert.AreEqual<int>(2, businessChancesNumber);           
        }
    }
}
