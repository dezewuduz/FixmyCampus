using FixmyCampus.Domain.Enums;

namespace FixmyCampus.Domain.Entities;

public class Ticket
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Building Building { get; set; } = Building.MainLibrary;
    public string Room { get; set; } = string.Empty;
    public TicketCategory Category { get; set; }
    public TicketPriority Priority { get; set; } = TicketPriority.Medium;
    public TicketStatus Status { get; set; } = TicketStatus.New;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }

    // Who reported it
    public int CreatedById { get; set; }
    public User CreatedBy { get; set; } = null!;

    // Who is fixing it (optional until assigned)
    public int? AssignedTechnicianId { get; set; }
    public Technician? AssignedTechnician { get; set; }

    public ICollection<TicketHistory> History { get; set; } = new List<TicketHistory>();
}