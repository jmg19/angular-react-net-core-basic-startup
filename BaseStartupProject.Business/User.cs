using BaseStartupProject.Business.Events;
using BaseStartupProject.Business.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Business
{
    public class User : BusinessBase<User>
    {
        private ILoginPasswordHasher hasher;

        private int _id;
        private string _username;
        private string _hash;
        private bool _active;

        public int id 
        { 
            get 
            { return _id; } 
            set 
            {
                if (!_isIdSetted)
                {
                    _id = value;
                    _isIdSetted = true;
                }
            } 
        }
        public string username { get { return _username; } }
        public string hash { get { return _hash; } }
        public bool active { get { return _active; } }

        public User(string username, IBusinessDataMediator businessEvents) : base(businessEvents)
        {            
            _username = username;            
            _active = true;
            hasher = new LoginPasswordHasher();
            _isIdSetted = false;
        }
        public User(string username, ILoginPasswordHasher hasher, IBusinessDataMediator businessEvents) : base(businessEvents)
        {
            _username = username;
            _active = true;
            this.hasher = hasher;
            _isIdSetted = false;
        }
        public User(int id, string username, string hash, bool active, IBusinessDataMediator businessEvents) : base(businessEvents)
        {
            _id = id;
            _username = username;
            _hash = hash;
            _active = active;
            hasher = new LoginPasswordHasher();
            _isIdSetted = true;
        }
        public User(int id, string username, string hash, bool active, ILoginPasswordHasher hasher, IBusinessDataMediator businessEvents) : base(businessEvents)
        {
            _id = id;
            _username = username;
            _hash = hash;
            _active = active;
            this.hasher = hasher;
            _isIdSetted = true;
        }
        
        public override User Cast()
        {
            return this;
        }

        protected override void RaiseBusinessChange(BusinessChangeType type)
        {
            _mediator.MediateBusinessDataChange<User>(this, type, this);
        }        

        public virtual bool DoLogin(string password)
        {
            string hash = hasher.GenerateUserHash(_id, _username, password);
            return hash == _hash;
        }

        public virtual void SetPassword(string newPassword)
        {            
            this._hash = hasher.GenerateUserHash(_id, _username, newPassword);
            RaiseBusinessChange(BusinessChangeType.Update);
        }

        public virtual void RecoverUserAccount()
        {
            
        }

        public virtual void Inactivate() {
            this._active = false;
            RaiseBusinessChange(BusinessChangeType.Update);
        }

        public virtual void Activate()
        {
            this._active = true;
            RaiseBusinessChange(BusinessChangeType.Update);
        }
    }
}
