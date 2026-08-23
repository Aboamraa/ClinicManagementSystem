using ClinicManagement.Application.Contracts.Repositories;
using ClinicManagement.Application.Contracts.Services;
using ClinicManagement.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.DependencyInjection
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDoctorService, DoctorService>();
            return services;
        }
    }
}