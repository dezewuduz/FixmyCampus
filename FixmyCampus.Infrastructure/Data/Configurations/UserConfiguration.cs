using FixmyCampus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FixmyCampus.Api.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(20);

        // Store the enum as readable text instead of a number
        builder.Property(u => u.Role)
            .HasConversion<string>()
            .HasMaxLength(20);

        // One Users table for User, Technician and Admin (TPH)
        builder.HasDiscriminator<string>("UserType")
            .HasValue<User>("User")
            .HasValue<Technician>("Technician")
            .HasValue<Admin>("Admin");
    }
}