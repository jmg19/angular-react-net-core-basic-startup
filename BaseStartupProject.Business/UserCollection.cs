using BaseStartupProject.Business.Events;
using System;
using System.Linq;

namespace BaseStartupProject.Business
{
    public class UserCollection : BusinessBaseCollection<User>
    {
        public UserCollection(IBusinessDataMediator businessEvents): base(businessEvents)
        {

        }
        public virtual User Get(string userName)
        {
            string[] conditions = { string.Format("UserName == \"{0}\"", userName) };
            User user = _mediator.MediateBusinessDataConsult<User>(this, conditions).FirstOrDefault();

            if (user != null) {                             
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

            User newUser = new User(userName, this._mediator);
            
            Add(newUser);
            return newUser;            
        }

        protected override void Add(User obj)
        {
            _mediator.MediateBusinessDataChange<User>(this, BusinessChangeType.Add, obj);
        }
    }
}
