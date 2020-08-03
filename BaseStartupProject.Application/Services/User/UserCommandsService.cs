using BaseStartupProject.Application.Commands;
using BaseStartupProject.Application.Commands.Users;

namespace BaseStartupProject.Application.Services.User
{
    public class UserCommandsService : IUserCommandsService
    {
        ICommandsHandleresFactory commandHandlerFactory;

        public UserCommandsService()
        {
            commandHandlerFactory = new CommandsHandleresFactory();
        }

        public UserCommandsService(ICommandsHandleresFactory commandHandlerFactory)
        {
            this.commandHandlerFactory = commandHandlerFactory;
        }

        public void Activate(ActivateUser command)
        {
            ICommandHandler<ActivateUser> commandHandler = commandHandlerFactory.CreateCommandHandler<ActivateUser>();
            commandHandler.Handle(command);
        }

        public void Inactivate(InactivateUser command)
        {
            ICommandHandler<InactivateUser> commandHandler = commandHandlerFactory.CreateCommandHandler<InactivateUser>();
            commandHandler.Handle(command);
        }

        public int Login(UserLogin command)
        {
            ICommandHandler<UserLogin> commandHandler = commandHandlerFactory.CreateCommandHandler<UserLogin>();
            commandHandler.Handle(command);
            return command.CommandResult;
        }

        public void SignUpNewUser(SignUpUser command)
        {
            ICommandHandler<SignUpUser> commandHandler = commandHandlerFactory.CreateCommandHandler<SignUpUser>();
            commandHandler.Handle(command);
        }
    }
}
