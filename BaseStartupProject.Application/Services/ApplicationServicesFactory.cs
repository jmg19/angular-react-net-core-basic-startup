using BaseStartupProject.Application.Services.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Services
{
    public class ApplicationServicesFactory : IApplicationServicesFactory
    {
        public ISessionTokenService CreateSessionTokenService()
        {
            return new SessionTokenService();
        }

        public IUserCommandsService CreateUserCommandsService()
        {
            return new UserCommandsService();
        }

        public IUserQueriesService CreateUserQueriesService()
        {
            return new UserQueriesService();
        }
    }
}
