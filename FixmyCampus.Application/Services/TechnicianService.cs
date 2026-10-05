using FixmyCampus.Application.DTOs.Technicians;
using FixmyCampus.Application.Interfaces;
using FixmyCampus.Domain.Entities;
using FixmyCampus.Domain.Enums;

namespace FixmyCampus.Application.Services;

public class TechnicianService : ITechnicianService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public TechnicianService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Technician> CreateAsync(
        CreateTechnicianDto dto)
    {
        var existingUser =
            await _userRepository.GetByEmailAsync(dto.Email);

        if (existingUser != null)
        {
            throw new InvalidOperationException(
                "Email is already registered.");
        }

        var technician = new Technician
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,

            Role = UserRole.Technician,

            IsActive = true,

            Specialization = dto.Specialization,

            IsAvailable = dto.IsAvailable,

            CreatedAt = DateTime.UtcNow
        };

        technician.PasswordHash =
            _passwordHasher.HashPassword(
                technician,
                dto.Password);

        await _userRepository.AddAsync(technician);

        await _userRepository.SaveChangesAsync();

        return technician;
    }

    public async Task<IEnumerable<Technician>> GetAllAsync()
    {
        // We will implement technician repository
        // properly in the next step.
        return new List<Technician>();
    }
}