using ClinicManagement.Application.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Domain.Contracts.Specifications
{
    // 4kl al parameters w al options al htgele f al query
    public interface ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        List<Expression<Func<TEntity, object>>> Includes { get; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; }
        Expression<Func<TEntity, object>>? OrderBy { get; }
        Expression<Func<TEntity, bool>>? Criteria { get; }

        int Take { get; }
        int Skip { get; }
        public bool IsPagingEnabled { get; }

        
    }
}
