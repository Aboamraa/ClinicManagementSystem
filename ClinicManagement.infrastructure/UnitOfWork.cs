using ClinicManagement.Application.Contracts.Repositories;
using ClinicManagement.Application.Entities.Abstract;
using ClinicManagement.Domain.Contracts;
using ClinicManagement.Infrastructure.Data;
using ClinicManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Infrastructure
{
    public class UnitOfWork(ClinicDbContext dbContext) : IUnitOfWork
    {
        private readonly Dictionary<Type, object> repositories = new();
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            if (repositories.TryGetValue(typeof(TEntity), out var repository))
                return (IGenericRepository<TEntity, TKey>)repository;
            var repo = new GenericRepository<TEntity, TKey>(dbContext);
            repositories[typeof(TEntity)] = repo;
            return repo;
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await dbContext.SaveChangesAsync(ct);
    }
}
