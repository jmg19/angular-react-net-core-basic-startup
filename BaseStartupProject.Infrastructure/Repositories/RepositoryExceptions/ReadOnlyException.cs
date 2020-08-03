using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Infrastructure.Repositories.RepositoryExceptions
{
    public class ReadOnlyException: Exception
    {
        public ReadOnlyException() : base("Just ReadOnly actions can be called!")
        {

        }
    }
}
