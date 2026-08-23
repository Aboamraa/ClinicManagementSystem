using ClinicManagement.Application.Entities.Abstract;
using ClinicManagement.Domain.Contracts.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Infrastructure.Specifications
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> baseQuery, ISpecification<TEntity, TKey> specs) where TEntity : BaseEntity<TKey>
        {
            var query = baseQuery;

            if (specs.Criteria != null)
                query = query.Where(specs.Criteria);


            if (specs.Includes.Count > 0)
            {
                query = specs.Includes.Aggregate(query, (current, include) => current.Include(include));
                //foreach (var include in specs.Includes)
                //{
                //    query = query.Include(include);
                //}
            }

            if (specs.OrderBy != null)
            {
                query = query.OrderBy(specs.OrderBy);
            }
            else if (specs.OrderByDescending != null)
            {
                query = query.OrderByDescending(specs.OrderByDescending);
            }

            if (specs.IsPagingEnabled)
            {
                query = query.Skip(specs.Skip).Take(specs.Take);
            }



            return query;
        }
        public static IQueryable<TEntity> CreateCountQuery<TEntity, TKey>(IQueryable<TEntity> baseQuery, ISpecification<TEntity, TKey> specs) where TEntity : BaseEntity<TKey>
        {
            var query = baseQuery;
            if (specs.Criteria != null)
                return query.Where(specs.Criteria);

            return query;
        }

    }
}
