using BaseStartupProject.Application.AppModels.ResultModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Services.User
{
    public interface ISessionTokenService
    {
        string GenerateToken(string UUID, UserAppModel userModel, bool isExpirable);
        UserAppModel DecryptToken(string UUID, string tokenString);
    }
}
