using BaseStartupProject.Application.AppModels.ResultModels;
using Global.SessionTokenGeneratorPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Mappers
{
    public class SessionTokenToUserAppModelMapper : AbstractMapper<SessionToken, UserAppModel>
    {
        public override UserAppModel Map(SessionToken inObject)
        {
            return new UserAppModel
            {
                id = inObject.userId,
                username = inObject.userName,
                active = true
            };
        }
    }
}
