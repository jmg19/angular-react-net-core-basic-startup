using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Application.Commands.Users
{
    public class SignUpUser : IComand
    {
        public SignUpUser()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }        
    }
}
