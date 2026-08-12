using ClinicManagement.Domain.Contracts.Repositories;
using ClinicManagement.Domain.Entities.Abstract;
using ClinicManagement.Infrastructure.Data;
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
        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
            => await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();


        public async Task<TEntity?> GetByIdAsync(TKey id)
           => await _dbContext.Set<TEntity>().FindAsync(id);
        public async Task AddAsync(TEntity entity)
            => await _dbContext.Set<TEntity>().AddAsync(entity);
        public void Update(TEntity entity)
            => _dbContext.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);

    }
}
