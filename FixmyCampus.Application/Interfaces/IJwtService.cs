using FixmyCampus.Domain.Entities;

namespace FixmyCampus.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}