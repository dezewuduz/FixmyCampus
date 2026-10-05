using FixmyCampus.Application.Interfaces;
using FixmyCampus.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace FixmyCampus.Infrastructure.Services;

public class PasswordHasherService : IPasswordHasher
{
    private readonly PasswordHasher<User> _hasher = new();

    public string HashPassword(
        User user,
        string password)
    {
        return _hasher.HashPassword(user, password);
    }

    public bool VerifyPassword(
        User user,
        string hashedPassword,
        string password)
    {
        var result = _hasher.VerifyHashedPassword(
            user,
            hashedPassword,
            password);

        return result != PasswordVerificationResult.Failed;
    }
}