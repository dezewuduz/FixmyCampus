using FixmyCampus.Application.Interfaces;
using FixmyCampus.Domain.Entities;
using FixmyCampus.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FixmyCampus.Infrastructure.Repositories;

public class TechnicianRepository : ITechnicianRepository
{
    private readonly AppDbContext _context;

    public TechnicianRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Technician technician)
    {
        await _context.Technicians.AddAsync(technician);
    }

    public async Task<Technician?> GetByIdAsync(int id)
    {
        return await _context.Technicians
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Technician?> GetByTechnicianIdAsync(
        int technicianId)
    {
        return await _context.Technicians
            .FirstOrDefaultAsync(t =>
                t.TechnicianId == technicianId);
    }

    public async Task<IEnumerable<Technician>> GetAllAsync()
    {
        return await _context.Technicians
            .OrderBy(t => t.FullName)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}