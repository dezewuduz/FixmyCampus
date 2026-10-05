using FixmyCampus.Domain.Enums;

namespace FixmyCampus.Domain.Entities;

public class TicketHistory
{
    public int Id { get; set; }

    public int TicketId { get; set; }
    public Ticket Ticket { get; set; } = null!;

    public TicketStatus OldStatus { get; set; }
    public TicketStatus NewStatus { get; set; }

    // Fixes your CS0246 error: a real ID plus a navigation to User
    public Guid ChangedById { get; set; }
    public User ChangedBy { get; set; } = null!;

    public string? Comment { get; set; }
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
}