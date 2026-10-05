using FixmyCampus.Domain.Entities;

namespace FixmyCampus.Application.Interfaces;

public interface ITechnicianRepository
{
    Task AddAsync(Technician technician);

    Task<Technician?> GetByIdAsync(int id);

    Task<Technician?> GetByTechnicianIdAsync(int technicianId);

    Task<IEnumerable<Technician>> GetAllAsync();

    Task SaveChangesAsync();
}