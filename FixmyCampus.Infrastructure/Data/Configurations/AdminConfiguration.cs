using FixmyCampus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FixmyCampus.Api.Data.Configurations;

public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.Property(a => a.Specialization)
            .HasMaxLength(100);
        builder.WithOne(a => a.Admin)
        .withmany()
            .HasForeignKey<Admin>(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}