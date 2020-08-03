using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Commands.Users
{
    public abstract class UserCommand : ICommand
    {
        public UserCommand()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }

    public class ActivateUser : UserCommand
    {
        public int userId { get; set; }
    }

    public class InactivateUser : UserCommand
    {
        public int userId { get; set; }
    }
}
