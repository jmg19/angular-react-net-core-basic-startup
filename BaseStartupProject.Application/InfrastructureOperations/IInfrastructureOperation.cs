using BaseStartupProject.Business.Events;
using BaseStartupProject.Infrastructure.Repositories;
using BaseStartupProject.Infrastructure.Repositories.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Application.InfrastructureOperations
{
    public interface IInfrastructureOperation
    {
        void Execute();     
    }
}
