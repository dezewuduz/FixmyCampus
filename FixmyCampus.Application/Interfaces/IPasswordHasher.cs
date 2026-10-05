using FixmyCampus.Domain.Entities;

namespace FixmyCampus.Application.Interfaces;

public interface IPasswordHasher
{
    string HashPassword(User user, string password);

    bool VerifyPassword(
        User user,
        string hashedPassword,
        string password);
}