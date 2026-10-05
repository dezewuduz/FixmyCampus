using FixmyCampus.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FixmyCampus.Application.DTOs.Tickets;

public class CreateTicketDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Location { get; set; } = string.Empty;

    [Required]
    public TicketCategory Category { get; set; }

    public TicketPriority Priority { get; set; } = TicketPriority.Medium;
}