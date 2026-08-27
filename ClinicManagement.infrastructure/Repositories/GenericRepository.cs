using ClinicManagement.Application.Contracts.Repositories;
using ClinicManagement.Application.Entities.Abstract;
using ClinicManagement.Domain.Contracts.Specifications;
using ClinicManagement.Infrastructure.Data;
using ClinicManagement.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Infrastructure.Repositories
{
    public class GenericRepository<TEntity, TKey>(ClinicDbContext _dbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity, TKey>? specs = null, CancellationToken ct = default)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            if (specs != null)
                return await SpecificationEvaluator.CreateQuery(query, specs).ToListAsync(ct);

            return await query.ToListAsync(ct);

        }


        public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity, TKey> specs, CancellationToken ct = default)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();


            return await SpecificationEvaluator.CreateQuery(query, specs).FirstOrDefaultAsync(ct);

        }
        public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default) => await _dbContext.Set<TEntity>().FindAsync(id, ct);

        public async Task AddAsync(TEntity entity, CancellationToken ct = default)
            => await _dbContext.Set<TEntity>().AddAsync(entity, ct);
        public void Update(TEntity entity)
            => _dbContext.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);

        public async Task<int> CountAsync(ISpecification<TEntity, TKey> specs, CancellationToken ct = default)
            => await SpecificationEvaluator.CreateCountQuery(_dbContext.Set<TEntity>(), specs).CountAsync(ct);

        public async Task<bool> IsExistsAsync(TKey id, CancellationToken ct = default) => await _dbContext.Set<TEntity>().AnyAsync(entity => entity.Id!.Equals(id), cancellationToken: ct);


    }
}
