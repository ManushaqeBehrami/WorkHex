using HexaFullStack.Application.Abstractions.Persistence;
using HexaFullStack.Application.Abstractions.Security;

namespace HexaFullStack.Application.UseCases.Users;

public sealed class LoginUser
{
    private readonly IUserRepository _users;
    private readonly IPasswordHasher _hasher;
    private readonly IJwtTokenService _jwt;

    public LoginUser(IUserRepository users, IPasswordHasher hasher, IJwtTokenService jwt)
    {
        _users = users; _hasher = hasher; _jwt = jwt;
    }

    public sealed record Command(string Email, string Password);
    public sealed record Result(string AccessToken, string RefreshToken, DateTime ExpiresUtc, string Role);

    public async Task<Result> HandleAsync(Command cmd, CancellationToken ct)
    {
        var user = await _users.FindByEmailAsync(cmd.Email, ct) ?? throw new UnauthorizedAccessException();
        if (!_hasher.Verify(user.PasswordHash, cmd.Password)) throw new UnauthorizedAccessException();
        var (at, rt, exp) = _jwt.CreateTokens(user.Id, user.Email, user.Role);
        return new(at, rt, exp, user.Role);
    }
}
