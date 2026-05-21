using Domain.Contracts;
using Domain.Entities;
using System.Linq.Expressions;

namespace Service.Specifications
{
    internal abstract class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        #region Where Expression
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
        protected BaseSpecification(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }
        #endregion

        #region Includ Expressions
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new List<Expression<Func<TEntity, object>>>();
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
            => IncludeExpressions.Add(includeExpression);
        #endregion

        #region Order By Expressions
        // Order By Ascending
        public Expression<Func<TEntity, object>>? OrderByAscending { get; private set; }
        protected void AddOrderByAscending(Expression<Func<TEntity, object>> orderByExpression)
            => OrderByAscending = orderByExpression;
        // Order By Descending
        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByExpression)
            => OrderByDescending = orderByExpression;
        #endregion

        #region Pagination
        public bool IsPaginated { get; set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }
        protected void ApplyPagination(int pageSize, int pageIndex) // Pagination constructor
        {
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
            IsPaginated = true;
        }

        #endregion
    }
}
