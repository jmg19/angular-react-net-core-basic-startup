using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Exceptions
{
    public class UserAlreadyInUseException : Exception
    {
        public UserAlreadyInUseException():base("This username is already used."){}
    }
}
