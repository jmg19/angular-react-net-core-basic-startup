using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Commands
{
    public interface ICommand
    {
        Guid Id { get; }        
    }
}
