using FixmyCampus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FixmyCampus.Api.Data.Configurations;

public class TechnicianConfiguration : IEntityTypeConfiguration<Technician>
{
    public void Configure(EntityTypeBuilder<Technician> builder)
    {
        builder.Property(t => t.Specialization)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.HasMany(t => t.AssignedTickets)
            .WithOne(t => t.AssignedTechnician)
            .HasForeignKey(t => t.AssignedTechnicianId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}