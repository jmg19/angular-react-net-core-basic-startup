using BaseStartupProject.Application.AppModels.ResultModels;
using BaseStartupProject.Infrastructure.Repositories.DataTableObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Mappers
{
    public class DtoUserToUserAppModelMapper : IMapper<DtoUser, UserAppModel>
    {
        public UserAppModel Map(DtoUser inObject)
        {
            return new UserAppModel
            {
                id = inObject.ID,
                username = inObject.UserName,
                active = inObject.Active
            };
        }

        public IList<UserAppModel> Map(IEnumerable<DtoUser> inObject)
        {
            List<UserAppModel> list = new List<UserAppModel>();
            foreach (DtoUser dto in inObject) {
                list.Add(new UserAppModel
                {
                    id = dto.ID,
                    username = dto.UserName,
                    active = dto.Active
                });
            }
            return list;
        }
    }
}
