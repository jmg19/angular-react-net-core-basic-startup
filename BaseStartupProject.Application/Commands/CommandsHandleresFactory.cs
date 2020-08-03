using BaseStartupProject.Application.Commands.Users;
using BaseStartupProject.Application.Queries;
using BaseStartupProject.Application.Queries.Users;
using BaseStartupProject.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Commands
{
    public class CommandsHandleresFactory : ICommandsHandleresFactory
    {
        public ICommandHandler<T> CreateCommandHandler<T>()
        {
            if (typeof(T).IsSubclassOf(typeof(UserCommand)))
                return (ICommandHandler<T>)(new UsersComandHandler());            

            return null;
        }
    }
}
