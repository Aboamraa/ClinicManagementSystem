using ClinicManagement.Application.Entities.Abstract;
using ClinicManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ClinicManagement.Infrastructure.DataSeeding
{
    public class IdentityDataSeeder(RoleManager<IdentityRole<Guid>> roleManager, UserManager<ApplicationUser> userManager, ILogger<IdentityDataSeeder> logger)
    {
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            var roles = new List<IdentityRole<Guid>>
            {
                new() { Name = "Admin", NormalizedName = "ADMIN" },
                new() { Name = "Doctor", NormalizedName = "DOCTOR" },
                new() { Name = "Patient", NormalizedName = "PATIENT" },
            };


            var users = new[]
            {
                new
                {
                    User = new ApplicationUser
                    {
                        Id = Guid.Parse("d91e5a43-6c27-48b9-8f15-3a72c0e61458"),
                        Email = "admin@clinic.com",
                        UserName = "admin",
                        PhoneNumber = "1234567890",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true
                    },
                    Role = "Admin"
                },

                new
                {
                    User = new ApplicationUser
                    {
                        Id = Guid.Parse("7f3c9d21-5b64-4a18-9e72-1c8f6b3d2045"),
                        Email = "doctor@clinic.com",
                        UserName = "doctor"
                    },
                    Role = "Doctor"
                },

                new
                {
                    User = new ApplicationUser
                    {
                        Id = Guid.Parse("b2a84e67-193f-4d5c-a8b1-7e2f9364c150"),
                        Email = "patient@clinic.com",
                        UserName = "patient"
                    },
                    Role = "Patient"
                }
            };

            // Seeding Roles
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name!))
                {
                    var result = await roleManager.CreateAsync(role);
                    if (!result.Succeeded)
                    {
                        logger.LogError("Failed to create role {RoleName}. Errors: {Errors}", role.Name, string.Join(", ", result.Errors.Select(e => e.Description)));
                        throw new Exception($"Failed to create role {role.Name}. Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
                else { logger.LogWarning("Role {RoleName} already exists.", role.Name); }
            }

            //Seeding Users
            //foreach (var user in users)
            //{
            //    if (await userManager.FindByEmailAsync(user.User.Email!) == null)
            //    {
            //        var result = await userManager.CreateAsync(user.User, "Password123!");
            //        if (!result.Succeeded) { logger.LogError("Failed to create user {Email}. Errors: {Errors}", user.User.Email, string.Join(", ", result.Errors.Select(e => e.Description))); }
            //    }
            //    else { logger.LogWarning("User with email {Email} already exists.", user.User.Email); }
            //}



            foreach (var user in users)
            {
                var existingUser = await userManager.FindByEmailAsync(user.User.Email!);
                if (existingUser == null)
                {
                    var result = await userManager.CreateAsync(user.User, "Password123!");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user.User, user.Role);
                        logger.LogInformation("User {Email} created and assigned to role {Role}.", user.User.Email, user.Role);
                    }
                    else
                    {
                        logger.LogError("Failed to create user {Email}. Errors: {Errors}", user.User.Email, string.Join(", ", result.Errors.Select(e => e.Description)));
                        throw new Exception($"Failed to create user {user.User.Email}. Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
                else
                {
                    logger.LogWarning("User with email {Email} already exists.", user.User.Email);
                }
            }
        }




    }
}
