using FixmyCampus.Application.DTOs.Tickets;
using FixmyCampus.Application.Interfaces;
using FixmyCampus.Domain.Entities;
using FixmyCampus.Domain.Enums;

namespace FixmyCampus.Application.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;

    public TicketService(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<TicketResponseDto> CreateAsync(
        CreateTicketDto dto,
        int userId)
    {
        var ticket = new Ticket
        {
            Title = dto.Title,
            Description = dto.Description,
           Building = dto.Building,
Room = dto.Room,
            Category = dto.Category,
            Priority = dto.Priority,
            Status = TicketStatus.New,
            CreatedById = userId,
            CreatedAt = DateTime.UtcNow
        };

        await _ticketRepository.AddAsync(ticket);
        await _ticketRepository.SaveChangesAsync();

        return await MapToDtoAsync(ticket)
            ?? throw new InvalidOperationException(
                "Ticket could not be created.");
    }

    public async Task<IEnumerable<TicketResponseDto>> GetAllAsync()
    {
        var tickets = await _ticketRepository.GetAllAsync();

        return tickets.Select(MapToDto);
    }

    public async Task<TicketResponseDto?> GetByIdAsync(int id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);

        return ticket == null ? null : MapToDto(ticket);
    }

    public async Task<IEnumerable<TicketResponseDto>> GetMyTicketsAsync(
        int userId)
    {
        var tickets =
            await _ticketRepository.GetByUserIdAsync(userId);

        return tickets.Select(MapToDto);
    }

    public async Task<TicketResponseDto?> UpdateAsync(
        int id,
        UpdateTicketDto dto,
        int userId)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);

        if (ticket == null || ticket.CreatedById != userId)
            return null;

        ticket.Title = dto.Title;
        ticket.Description = dto.Description;
        ticket.Building = dto.Building;
        ticket.Room = dto.Room;
        ticket.Category = dto.Category;
        ticket.Priority = dto.Priority;
        ticket.UpdatedAt = DateTime.UtcNow;

        await _ticketRepository.UpdateAsync(ticket);
        await _ticketRepository.SaveChangesAsync();

        return MapToDto(ticket);
    }

    public async Task<bool> DeleteAsync(
        int id,
        int userId)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);

        if (ticket == null || ticket.CreatedById != userId)
            return false;

        await _ticketRepository.DeleteAsync(ticket);
        await _ticketRepository.SaveChangesAsync();

        return true;
    }

    public async Task<TicketResponseDto?> AssignAsync(
        int ticketId,
        AssignTicketDto dto)
    {
        var ticket =
            await _ticketRepository.GetByIdAsync(ticketId);

        if (ticket == null)
            return null;

        var technician =
            await _ticketRepository
                .GetTechnicianByTechnicianIdAsync(
                    dto.TechnicianId);

        if (technician == null)
            throw new InvalidOperationException(
                "Technician not found.");

        if (!technician.IsAvailable)
            throw new InvalidOperationException(
                "Technician is not available.");

        ticket.AssignedTechnicianId = technician.Id;
        ticket.Status = TicketStatus.Assigned;
        ticket.UpdatedAt = DateTime.UtcNow;

        await _ticketRepository.UpdateAsync(ticket);
        await _ticketRepository.SaveChangesAsync();

        return MapToDto(ticket);
    }

    public async Task<TicketResponseDto?> UpdateStatusAsync(
        int ticketId,
        TicketStatus status,
        int changedById,
        string? comment)
    {
        var ticket =
            await _ticketRepository.GetByIdAsync(ticketId);

        if (ticket == null)
            return null;

        var oldStatus = ticket.Status;

        ticket.Status = status;
        ticket.UpdatedAt = DateTime.UtcNow;

        if (status == TicketStatus.Resolved)
        {
            ticket.ResolvedAt = DateTime.UtcNow;
        }

        var history = new TicketHistory
        {
            TicketId = ticket.Id,
            OldStatus = oldStatus,
            NewStatus = status,
            ChangedById = changedById,
            Comment = comment,
            ChangedAt = DateTime.UtcNow
        };

        await _ticketRepository.AddHistoryAsync(history);
        await _ticketRepository.UpdateAsync(ticket);
        await _ticketRepository.SaveChangesAsync();

        return MapToDto(ticket);
    }

    private static TicketResponseDto MapToDto(Ticket ticket)
    {
        return new TicketResponseDto
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
           Building = ticket.Building,
Room = ticket.Room,
            Category = ticket.Category,
            Priority = ticket.Priority,
            Status = ticket.Status,
            CreatedAt = ticket.CreatedAt,
            UpdatedAt = ticket.UpdatedAt,
            ResolvedAt = ticket.ResolvedAt,

            CreatedById = ticket.CreatedById,
            CreatedByName = ticket.CreatedBy?.FullName,

            AssignedTechnicianId =
                ticket.AssignedTechnician?.TechnicianId,

            AssignedTechnicianName =
                ticket.AssignedTechnician?.FullName
        };
    }

    private async Task<TicketResponseDto?> MapToDtoAsync(
        Ticket ticket)
    {
        var savedTicket =
            await _ticketRepository.GetByIdAsync(ticket.Id);

        return savedTicket == null
            ? null
            : MapToDto(savedTicket);
    }

   public async Task<IEnumerable<TicketResponseDto>> GetFilteredAsync(
    Building? building,
    TicketStatus? status)
{
    var tickets =
        await _ticketRepository.GetFilteredAsync(
            building,
            status);

    return tickets.Select(MapToDto);
}
}