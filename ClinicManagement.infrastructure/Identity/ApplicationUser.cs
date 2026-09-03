using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        //public string Email { get; set; } = default!;
        //public string PhoneNumber { get; set; } = default!;
        //public string PasswordHash { get; set; } = default!;
        //public string UserName { get; set; } = default!;
        //public ICollection<string> Roles { get; set; } = new List<string>();
    }
}
