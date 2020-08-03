using BaseStartupProject.Application.Commands;
using BaseStartupProject.Application.Queries;
using BaseStartupProject.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Commands
{
    public interface ICommandsHandleresFactory
    {
        ICommandHandler<T> CreateCommandHandler<T>();
    }
}
