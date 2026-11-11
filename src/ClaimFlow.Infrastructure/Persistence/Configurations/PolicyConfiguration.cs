using ClaimFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimFlow.Infrastructure.Persistence.Configurations
{
    public class PolicyConfiguration : IEntityTypeConfiguration<Policy>
    {
        public void Configure(EntityTypeBuilder<Policy> builder)
        {
            builder.ToTable("Policies");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.PolicyNumber)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(p => p.CoverageAmount)
                .HasPrecision(18, 2);
            builder.Property(p => p.Premium)
                .HasPrecision(18, 2);
            builder.Property(p => p.Status)
                .HasConversion<string>()
                .HasMaxLength(20);


            builder.HasIndex(p => p.PolicyNumber)
                .IsUnique();
        }
    }
    
}
