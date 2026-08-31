using ClinicManagement.API.ExceptionHandling;
using ClinicManagement.Application.DependencyInjection;
using ClinicManagement.Application.Entities;
using ClinicManagement.Domain.Contracts;
using ClinicManagement.Infrastructure.DataSeeding;
using ClinicManagement.Infrastructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicManagement
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Custom Services Registration
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();

            // Add (register) Exception handler
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();
            var app = builder.Build();

            // For DataSeeding
            using var dataSeeder = app.Services.CreateScope();

            var seeder = dataSeeder.ServiceProvider.GetRequiredService<IDataSeeder>();
            await seeder.SeedDataAsync<Department, int>("Departments.json", true);
            await seeder.SeedDataAsync<Doctor, int>("Doctors.json", true);
            await seeder.SeedDataAsync<Patient, Guid>("Patients.json", false);
            await seeder.SeedDataAsync<Appointment, Guid>("Appointments.json", false);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseExceptionHandler();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
