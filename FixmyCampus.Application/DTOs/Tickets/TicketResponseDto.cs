using FixmyCampus.Domain.Enums;

namespace FixmyCampus.Application.DTOs.Tickets;

public class TicketResponseDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public TicketCategory Category { get; set; }

    public TicketPriority Priority { get; set; }

    public TicketStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public Guid CreatedById { get; set; }

    public string? CreatedByName { get; set; }

    public int AssignedTechnicianId { get; set; }

    public string? AssignedTechnicianName { get; set; }
}