using FixmyCampus.Domain.Entities;
using FixmyCampus.Domain.Enums;

namespace FixmyCampus.Application.Interfaces;

public interface ITicketRepository
{
    Task AddAsync(Ticket ticket);

    Task<Ticket?> GetByIdAsync(int id);

    Task<IEnumerable<Ticket>> GetAllAsync();

    Task<IEnumerable<Ticket>> GetByUserIdAsync(int userId);

   Task<IEnumerable<Ticket>> GetFilteredAsync(
    Building? building,
    TicketStatus? status);
    
    Task<Technician?> GetTechnicianByTechnicianIdAsync(
        int technicianId);

    Task UpdateAsync(Ticket ticket);

    Task DeleteAsync(Ticket ticket);

    Task AddHistoryAsync(TicketHistory history);

    Task SaveChangesAsync();
}