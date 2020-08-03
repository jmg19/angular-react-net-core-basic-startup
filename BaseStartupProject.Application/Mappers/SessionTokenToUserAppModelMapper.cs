using BaseStartupProject.Application.AppModels.ResultModels;
using Global.SessionTokenGeneratorPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Mappers
{
    public class SessionTokenToUserAppModelMapper : IMapper<SessionToken, UserAppModel>
    {
        public UserAppModel Map(SessionToken inObject)
        {
            return new UserAppModel
            {
                id = inObject.userId,
                username = inObject.userName,
                active = true
            };
        }

        public IList<UserAppModel> Map(IEnumerable<SessionToken> inObject)
        {
            List<UserAppModel> list = new List<UserAppModel>();
            foreach (SessionToken token in inObject)
            {
                list.Add(new UserAppModel
                {
                    id = token.userId,
                    username = token.userName,
                    active = true
                });
            }
            return list;
        }
    }
}
