using FixmyCampus.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FixmyCampus.Application.DTOs.Tickets;

public class UpdateTicketDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public Building Building { get; set; } = Building.MainLibrary;

    [Required]
    [MaxLength(50)]
    public string Room { get; set; } = string.Empty;

    public TicketCategory Category { get; set; }

    public TicketPriority Priority { get; set; }
}