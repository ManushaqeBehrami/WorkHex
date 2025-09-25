using Microsoft.EntityFrameworkCore;
using HexaFullStack.Application.Abstractions.Persistence;
using HexaFullStack.Domain.Entities;

namespace HexaFullStack.Infrastructure.Persistence.MySql;

public sealed class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    public UserRepository(AppDbContext db) => _db = db;

    public Task<bool> EmailExistsAsync(string email, CancellationToken ct)
        => _db.Users.AnyAsync(u => u.Email == email, ct);

    public async Task AddAsync(User user, CancellationToken ct)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync(ct);
    }

    public Task<User?> FindByEmailAsync(string email, CancellationToken ct)
        => _db.Users.FirstOrDefaultAsync(u => u.Email == email, ct);

    public Task<User?> FindByIdAsync(Guid id, CancellationToken ct)
        => _db.Users.FirstOrDefaultAsync(u => u.Id == id, ct);
}
