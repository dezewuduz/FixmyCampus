using FixmyCampus.Application.DTOs.Technicians;
using FixmyCampus.Domain.Entities;

namespace FixmyCampus.Application.Interfaces;

public interface ITechnicianService
{
    Task<Technician> CreateAsync(
        CreateTechnicianDto dto);

    Task<IEnumerable<Technician>> GetAllAsync();
}