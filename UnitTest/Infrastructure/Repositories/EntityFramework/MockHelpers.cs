using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseStartupProject.Tests.Infrastructure.Repositories.EntityFramework
{
    public class MockHelpers
    {
        public static DbSet<T> GetQueryableMockDbSet<T>(IList<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(x => x.Add(It.IsAny<T>())).Callback<T>((obj) => sourceList.Add(obj));
            dbSet.Setup(x => x.Remove(It.IsAny<T>())).Callback<T>((obj) => sourceList.Remove(obj));

            return dbSet.Object;
        }
    }
}
