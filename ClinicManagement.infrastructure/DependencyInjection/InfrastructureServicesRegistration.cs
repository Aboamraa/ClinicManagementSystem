using ClinicManagement.Application.Contracts.Repositories;
using ClinicManagement.Domain.Contracts;
using ClinicManagement.Infrastructure.Data;
using ClinicManagement.Infrastructure.DataSeeding;
using ClinicManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Infrastructure.DependencyInjection
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ClinicDbContext>(o => o.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            //services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDataSeeder, DataSeeder>();
            return services;
        }
    }
}
