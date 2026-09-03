using ClinicManagement.Application.Entities;
using ClinicManagement.Application.Entities.Abstract;
using ClinicManagement.Domain.Contracts;
using ClinicManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClinicManagement.Infrastructure.DataSeeding
{
    public class DataSeeder(ClinicDbContext context, ILogger<DataSeeder> logger) : IDataSeeder
    {
        public async Task SeedDataAsync<TEntity, TKey>(string fileName, bool insertExplicitIdentity, CancellationToken ct = default) where TEntity : BaseEntity<TKey>
        {
            try
            {
                // Check if database updated
                if ((await context.Database.GetPendingMigrationsAsync(cancellationToken: ct)).Any())
                    await context.Database.MigrateAsync(cancellationToken: ct);

                // Check if data already exists [Department]
                if (await context.Set<TEntity>().AnyAsync(cancellationToken: ct))
                {
                    logger.LogWarning("{Entity} Data already exists in the database. Skipping seeding.", typeof(TEntity).Name);
                    return;
                }

                // read data from JSON files
                var rootPath = AppContext.BaseDirectory;
                var filePath = Path.Combine(rootPath, "Data", "SeedData", fileName);

                var data = await ReadFile<TEntity, TKey>(filePath, ct);

                if (data is null || data.Count == 0)
                {
                    logger.LogWarning("No data found in the file: {filePath}", filePath);
                    return;
                }
                if (insertExplicitIdentity)
                {
                    await SeedWithExplicitIdentityAsync<TEntity, TKey>(data, "Departments", filePath, ct);
                }
                else
                {
                    // Add data to the database
                    await context.Set<TEntity>().AddRangeAsync(data, ct);

                    logger.LogInformation("Successfully seeded {dataCount} records to the database from file: {filePath}", data.Count, filePath);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database: {Message}", ex.Message);
                throw;
            }
            finally
            {
                await context.SaveChangesAsync(ct);
            }
        }

        private async Task<List<TEntity>?> ReadFile<TEntity, TKey>(string filePath, CancellationToken ct = default) where TEntity : BaseEntity<TKey>
        {

            if (!File.Exists(filePath))
            {
                logger.LogError("File not found: {filePath}", filePath);
                throw new FileNotFoundException($"File not found: {filePath}");
            }
            using var file = File.OpenRead(filePath);


            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true

            };
            options.Converters.Add(new JsonStringEnumConverter());

            var data = await JsonSerializer.DeserializeAsync<List<TEntity>>(file, options, ct);



            return data;

        }

        private async Task SeedWithExplicitIdentityAsync<TEntity, TKey>(List<TEntity> data, string tableName, string filePath, CancellationToken ct = default) where TEntity : BaseEntity<TKey>
        {
            await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {tableName} ON", ct);

            await context.Set<TEntity>().AddRangeAsync(data, ct);
            await context.SaveChangesAsync(ct);

            logger.LogInformation("Successfully seeded {dataCount} records to the database from file: {filePath}", data.Count, filePath);

            await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {tableName} OFF", ct);
        }

    }
}
