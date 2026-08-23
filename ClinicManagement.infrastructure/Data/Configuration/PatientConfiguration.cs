using ClinicManagement.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Infrastructure.Data.Configuration
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Email)
                .HasMaxLength(100);

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(14);

        }
    }
}
