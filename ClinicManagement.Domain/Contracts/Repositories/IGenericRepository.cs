using ClinicManagement.Application.Entities.Abstract;
using ClinicManagement.Domain.Contracts.Specifications;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Contracts.Repositories
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity, TKey>? specs, CancellationToken ct = default);
        Task<TEntity?> GetByIdAsync(ISpecification<TEntity, TKey> specs, CancellationToken ct = default);
        Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default);
        Task AddAsync(TEntity entity, CancellationToken ct = default);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<int> CountAsync(ISpecification<TEntity, TKey> specs, CancellationToken ct = default);

        Task<bool> IsExistsAsync(TKey id, CancellationToken ct = default);

        public Task<bool> IsExistsAsync(ISpecification<TEntity, TKey> specs, CancellationToken ct = default);

    }
}
