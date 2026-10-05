using FixmyCampus.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FixmyCampus.Application.DTOs.Technicians;

public class CreateTechnicianDto
{
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [Required]
    public TicketCategory Specialization { get; set; }

    public bool IsAvailable { get; set; } = true;
}