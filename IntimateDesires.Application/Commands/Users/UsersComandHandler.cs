using IntimateDesires.Application.InfrastructureOperationsDelegations;
using IntimateDesires.Business;
using IntimateDesires.Business.Events;

namespace IntimateDesires.Application.Commands.Users
{
    public class UsersComandHandler :
        AbstractComandHandler<SignUpUser>
    {
        private readonly IInfrastructureOperationDelegator delegator;

        public UsersComandHandler() : base()
        {
            delegator = new InfrastructureOperationDelegator();
        }

        public UsersComandHandler(IBusinessEventsFactory businessEventsFactory, IInfrastructureOperationDelegator delegator) : base(businessEventsFactory)
        {
            this.delegator = delegator;

        }

        public override void Handle(SignUpUser command)
        {
            IBusinessEvents businessEvents = businessEventsFactory.CreateBusinessEvents(delegator.handleBusinessNeed, delegator.handleBusinessNeed);

            UserCollection userCollection = new UserCollection(businessEvents);            
            
            User newUser = userCollection.CreateNewUser(command.UserName);
            
            newUser.SetPassword(command.Password);

            delegator.SaveChances();
        }
    }
}
