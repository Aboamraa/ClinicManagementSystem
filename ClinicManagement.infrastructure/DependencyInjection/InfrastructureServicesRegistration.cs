using ClinicManagement.Application.Contracts.Repositories;
using ClinicManagement.Domain.Contracts;
using ClinicManagement.Infrastructure.Data;
using ClinicManagement.Infrastructure.DataSeeding;
using ClinicManagement.Infrastructure.Identity;
using ClinicManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
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

            //For Identity configuration
            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ClinicDbContext>();

            services.AddScoped<IdentityDataSeeder>();

            return services;
        }
    }
}
