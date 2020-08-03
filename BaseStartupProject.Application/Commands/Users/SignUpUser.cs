using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Commands.Users
{
    public class SignUpUser : UserCommand
    {
        public SignUpUser() : base()
        {            
        }
        
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
