using BaseStartupProject.Application.Services.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Services
{
    public interface IApplicationServicesFactory
    {
        ISessionTokenService CreateSessionTokenService();

        #region == Users Services ==

        IUserCommandsService CreateUserCommandsService();
        IUserQueriesService CreateUserQueriesService();

        #endregion
    }
}
