using IntimateDesires.Business.Events;
using System;

namespace IntimateDesires.Business
{
    public class UserCollection : BusinessBaseCollection<User>
    {
        public UserCollection(IBusinessEvents businessEvents): base(businessEvents)
        {

        }
        internal virtual User Get(string userName)
        {
            string[] conditions = { string.Format("UserName == {0}", userName) };
            BusinessConsultEventArgs args = new BusinessConsultEventArgs(typeof(User)) { businessConsultType = BusinessConsultType.GetBy, conditions = conditions };
            _handlers.BusinessConsultNeed(this, args);

            if (args.entitiesListResult.Count > 0) {             
                User user = (User)(args.entitiesListResult[0]);
                if (user.id > 0)
                {
                    return user;
                }
            }

            return null;
        }

        public User CreateNewUser(string userName)
        {
            User user = Get(userName);
            if(user != null)
            {
                return null;
            }

            User newUser = new User(userName, this._handlers);
            
            Add(newUser);
            return newUser;            
        }

        protected override void Add(User obj)
        {
            _handlers.BusinessChangeNeed(this, new BusinessChangeEventArgs(typeof(User)) { businessChangeType = BusinessChangeType.Add, entity = obj });
        }
    }
}
