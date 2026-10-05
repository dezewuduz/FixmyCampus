using FixmyCampus.Application.DTOs.Tickets;
using FixmyCampus.Domain.Enums;

namespace FixmyCampus.Application.Interfaces;

public interface ITicketService
{
    // Create a new ticket
    Task<TicketResponseDto> CreateAsync(
        CreateTicketDto dto,
        int userId);

    // Get all tickets
    Task<IEnumerable<TicketResponseDto>> GetAllAsync();

    // Get one ticket
    Task<TicketResponseDto?> GetByIdAsync(int id);

    // Get tickets created by current user
    Task<IEnumerable<TicketResponseDto>> GetMyTicketsAsync(
        int userId);

    // Update a ticket
    Task<TicketResponseDto?> UpdateAsync(
        int id,
        UpdateTicketDto dto,
        int userId);

    // Delete a ticket
    Task<bool> DeleteAsync(
        int id,
        int userId);

    // Assign technician
    Task<TicketResponseDto?> AssignAsync(
        int ticketId,
        AssignTicketDto dto);

    // Change ticket status
    Task<TicketResponseDto?> UpdateStatusAsync(
        int ticketId,
        TicketStatus status,
        int changedById,
        string? comment);

      Task<IEnumerable<TicketResponseDto>> GetFilteredAsync(
    Building? building,
    TicketStatus? status);
}
