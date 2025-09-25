using HexaFullStack.Application.Abstractions.Persistence;
using HexaFullStack.Application.Abstractions.Security;
using HexaFullStack.Application.Abstractions.Realtime;
using HexaFullStack.Domain.Entities;

namespace HexaFullStack.Application.UseCases.Users;

public sealed class RegisterUser
{
    private readonly IUserRepository _users;
    private readonly IPasswordHasher _hasher;
    private readonly INotificationBus _bus;

    public RegisterUser(IUserRepository users, IPasswordHasher hasher, INotificationBus bus)
    {
        _users = users; _hasher = hasher; _bus = bus;
    }

    public sealed record Command(string Email, string FullName, string Password, string Role = "User");
    public sealed record Result(Guid Id, string Email, string FullName, string Role);

    public async Task<Result> HandleAsync(Command cmd, CancellationToken ct)
    {
        if (await _users.EmailExistsAsync(cmd.Email, ct)) throw new InvalidOperationException("Email already in use");
        var hash = _hasher.Hash(cmd.Password);
        var user = User.Create(cmd.Email, cmd.FullName, hash, cmd.Role);
        await _users.AddAsync(user, ct);
        await _bus.BroadcastAsync("users:registered", new { user.Id, user.Email, user.FullName }, ct);
        return new(user.Id, user.Email, user.FullName, user.Role);
    }
}
