using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace BaseStartupProject.Application.Commands.Users
{
    public class UserLogin : UserCommand, ICommandResult<int>
    {
        public UserLogin():base()
        {            
        }
        
        public string UserName { get; set; }
        public string Password { get; set; }        
        public int CommandResult { get; internal set; }
    }
}
