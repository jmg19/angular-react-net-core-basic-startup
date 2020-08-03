using System;
using System.Collections.Generic;

namespace BaseStartupProject.Business.Events
{
    public interface IBusinessDataMediator
    {        
        IEnumerable<T> MediateBusinessDataConsult<T>(object sender);
        IEnumerable<T> MediateBusinessDataConsult<T>(object sender, int entityId);
        IEnumerable<T> MediateBusinessDataConsult<T>(object sender, string[] conditions);
        void MediateBusinessDataChange<T>(object sender, BusinessChangeType type, BusinessObject entity);        
    }
}