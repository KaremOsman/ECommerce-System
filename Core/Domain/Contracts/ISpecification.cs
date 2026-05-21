using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Contracts
{
    public interface ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        // Property Signature for Each & Every Specification

        // Where Expression = Criteria
        Expression<Func<TEntity, bool>>? Criteria { get; }

        // Includes Expressions in One = IncludeExpressions
        List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }

        //Order By Ascending = OrderByAscending
        Expression<Func<TEntity, object>>? OrderByAscending { get; }

        // Order By Descending = OrderByDescending
        Expression<Func<TEntity, object>>? OrderByDescending { get; }
        
        // Pagination Properties
        public bool IsPaginated { get; set; }
        public int Take { get; }
        public int Skip { get; }


    }
}
