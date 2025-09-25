using Microsoft.AspNetCore.Identity;
using HexaFullStack.Application.Abstractions.Security;

namespace HexaFullStack.Infrastructure.Security;

public sealed class PasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<string> _hasher = new();
    public string Hash(string password) => _hasher.HashPassword(string.Empty, password);
    public bool Verify(string hash, string password)
        => _hasher.VerifyHashedPassword(string.Empty, hash, password) is PasswordVerificationResult.Success;
}
