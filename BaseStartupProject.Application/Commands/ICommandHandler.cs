using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.Commands
{
    public interface ICommandHandler<T>
    {
        public abstract void Handle(T command);
    }
}
