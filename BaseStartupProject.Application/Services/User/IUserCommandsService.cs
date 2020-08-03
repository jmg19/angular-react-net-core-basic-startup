using BaseStartupProject.Application.Commands.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Services.User
{
    public interface IUserCommandsService
    {
        void SignUpNewUser(SignUpUser command);
        int Login(UserLogin command);
        void Activate(ActivateUser command);
        void Inactivate(InactivateUser command);
    }
}
