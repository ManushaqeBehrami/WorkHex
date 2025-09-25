namespace HexaFullStack.Domain.Entities;

public sealed class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public string FullName { get; private set; } = default!;
    public string Role { get; private set; } = "User";
    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;

    private User() { }
    public static User Create(string email, string fullName, string passwordHash, string role = "User")
        => new() { Email = email, FullName = fullName, PasswordHash = passwordHash, Role = role };
}
