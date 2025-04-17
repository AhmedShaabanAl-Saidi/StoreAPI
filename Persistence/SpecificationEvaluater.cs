using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class SpecificationEvaluater
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> baseQuery , Specification<T> specification) where T : class
        {
            var query = baseQuery;

            if (specification.Criteria is not null)
                query = query.Where(specification.Criteria);

            query = specification.Includes.Aggregate(query, (currentQuery, include) => currentQuery.Include(include));

            return query;
        }
    }
}
