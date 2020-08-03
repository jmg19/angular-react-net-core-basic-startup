using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Commands
{
    interface ICommandResult<T>
    {
        T CommandResult { get; }
    }
}
