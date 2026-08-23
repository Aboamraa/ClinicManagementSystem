using ClinicManagement.Application.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Domain.Contracts
{
    public interface IDataSeeder
    {
        Task SeedDataAsync<TEntity, TKey>(string fileName, bool insertExplicitIdentity, CancellationToken ct = default) where TEntity : BaseEntity<TKey>;
    }
}
