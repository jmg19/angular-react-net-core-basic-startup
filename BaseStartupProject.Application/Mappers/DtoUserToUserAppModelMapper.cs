using BaseStartupProject.Application.AppModels.ResultModels;
using BaseStartupProject.Infrastructure.Repositories.DataTableObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Mappers
{
    public class DtoUserToUserAppModelMapper : AbstractMapper<DtoUser, UserAppModel>
    {
        public override UserAppModel Map(DtoUser inObject)
        {
            return new UserAppModel
            {
                id = inObject.ID,
                username = inObject.UserName,
                active = inObject.Active
            };
        }
    }
}
