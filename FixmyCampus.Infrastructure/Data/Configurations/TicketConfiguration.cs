using FixmyCampus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FixmyCampus.Api.Data.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(t => t.Location)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Category)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(t => t.Priority)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(t => t.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        // Reporter: don't allow deleting a user who has tickets
        builder.HasOne(t => t.CreatedBy)
            .WithMany(u => u.ReportedTickets)
            .HasForeignKey(t => t.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        // Technician relationship is configured in TechnicianConfiguration

        // Deleting a ticket deletes its history
        builder.HasMany(t => t.History)
            .WithOne(h => h.Ticket)
            .HasForeignKey(h => h.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        // Common query filters
        builder.HasIndex(t => t.Status);
        builder.HasIndex(t => t.CreatedById);
        builder.HasIndex(t => t.AssignedTechnicianId);
    }
}