using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    static class SpecificationEvaluator
    {
        // This method evaluates a specification against a queryable source and returns the filtered result.
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery, ISpecification<TEntity, TKey> specification) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;
            
            #region Building Where Expression
            // Apply the criteria from the specification if it exists
            if (specification.Criteria is not null)
                query = query.Where(specification.Criteria);

            #endregion

            #region Building Include Expressions
            // advaced syntax
            if (specification.IncludeExpressions is not null && specification.IncludeExpressions.Any())
                query = specification.IncludeExpressions.Aggregate(query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));

            /// syntax sugar
            ///if (specification.IncludeExpressions != null && specification.IncludeExpressions.Any())
            ///    specification.IncludeExpressions.ForEach(expression => query = query.Include(expression));

            /// old syntax
            ///if (specification.IncludeExpressions != null && specification.IncludeExpressions.Any())
            ///{
            ///    foreach (var expression in specification.IncludeExpressions)
            ///    {
            ///        query = query.Include(expression);
            ///    }
            ///} 

            #endregion

            #region Building OrderBy Expression
            if (specification.OrderByAscending is not null)
                query = query.OrderBy(specification.OrderByAscending);
            else if (specification.OrderByDescending is not null)
                query = query.OrderByDescending(specification.OrderByDescending);

            #endregion

            #region Building Pagingation
            if (specification.IsPaginated)
                query = query.Skip(specification.Skip).Take(specification.Take);

            #endregion

            return query;
        }
    }
}
