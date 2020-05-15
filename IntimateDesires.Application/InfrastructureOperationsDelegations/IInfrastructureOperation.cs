using IntimateDesires.Business.Events;
using IntimateDesires.Infrastructure.Repositories;
using IntimateDesires.Infrastructure.Repositories.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Application.InfrastructureOperationsDelegations
{
    public interface IInfrastructureOperation
    {
        void Execute();     
    }
}
