using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace IntimateDesires.Infrastructure.Repositories.Utils
{
    public static class Helper
    {


        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, OrderingRule[] ordering_rules)
        {
            bool first = true;            

            foreach (OrderingRule rule in ordering_rules)
            {
                var rootParameterExression = Expression.Parameter(typeof(T));

                Expression expression = rootParameterExression;

                PropertyInfo property = null;

                var properties = rule.field.Split('.');
                foreach (var propertyName in properties)
                {
                    if (property == null)
                    {
                        property = typeof(T).GetProperty(propertyName);
                    }
                    else
                    {
                        property = property.PropertyType.GetProperty(propertyName);
                    }

                    expression = Expression.Property(expression, propertyName);
                }
                var lambda = Expression.Lambda(expression, rootParameterExression);

                string command = "OrderBy";

                if (rule.descending && first)
                {
                    command = "OrderByDescending";
                }
                else if (!rule.descending && !first)
                {
                    command = "ThenBy";
                }
                else if (rule.descending && !first)
                {
                    command = "ThenByDescending";
                }

                first = false;

                Expression resultExpression = Expression.Call(
                    typeof(Queryable),
                    command,
                    new Type[] { typeof(T), property.PropertyType },
                    query.Expression,
                    Expression.Quote(lambda)
                   );

                query = query.Provider.CreateQuery<T>(resultExpression);
            }


            return query;
        }
    }
}
