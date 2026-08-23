using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ClinicManagement.Application.Entities.Abstract;
using ClinicManagement.Domain.Contracts.Specifications;

namespace ClinicManagement.Application.Specifications
{
    public class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public BaseSpecification(Expression<Func<TEntity, bool>>? criteria = null)
        {

            Criteria = criteria;
        }

        public List<Expression<Func<TEntity, object>>> Includes { get; } = [];

        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

        public Expression<Func<TEntity, bool>>? Criteria { get; }

        public int Take { get; private set; } = 0;

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }


        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}
