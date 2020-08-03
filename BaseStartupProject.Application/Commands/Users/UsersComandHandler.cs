using BaseStartupProject.Application.Exceptions;
using BaseStartupProject.Application.InfrastructureOperations;
using BaseStartupProject.Business;
using BaseStartupProject.Business.Events;

namespace BaseStartupProject.Application.Commands.Users
{
    public class UsersComandHandler : AbstractComandHandler,
        ICommandHandler<SignUpUser>,
        ICommandHandler<UserLogin>,
        ICommandHandler<ActivateUser>,
        ICommandHandler<InactivateUser>
    {        
        public UsersComandHandler() : base()
        {
        }

        public UsersComandHandler(IMediatorsFactory mediatorsFactory, IInfrastructureOperationsFactory infrastructureOperationsFactory) : base(mediatorsFactory, infrastructureOperationsFactory)
        {
        }

        public void Handle(SignUpUser command)
        {
            UserCollection userCollection = new UserCollection(businessDataMediator);            
            
            User newUser = userCollection.CreateNewUser(command.UserName);

            if (newUser == null) {
                throw new UserAlreadyInUseException();
            }                       

            newUser.SetPassword(command.Password);

            SaveChances();
        }

        public void Handle(UserLogin command)
        {
            UserCollection userCollection = new UserCollection(businessDataMediator);
            User user = userCollection.Get(command.UserName);
            if (user != null && user.DoLogin(command.Password)){
                command.CommandResult = user.id;
            }
        }

        public void Handle(InactivateUser command)
        {
            UserCollection userCollection = new UserCollection(businessDataMediator);
            User user = userCollection.Get(command.userId);
            if (user != null)
            {
                user.Inactivate();
                SaveChances();
            }
        }

        public void Handle(ActivateUser command)
        {
            UserCollection userCollection = new UserCollection(businessDataMediator);
            User user = userCollection.Get(command.userId);
            if (user != null)
            {
                user.Activate();
                SaveChances();
            }
        }
    }
}
