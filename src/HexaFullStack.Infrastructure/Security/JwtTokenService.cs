using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using HexaFullStack.Application.Abstractions.Security;

namespace HexaFullStack.Infrastructure.Security;

public sealed class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _s;
    public JwtTokenService(JwtSettings s) => _s = s;

    public (string accessToken, string refreshToken, DateTime expiresUtc) CreateTokens(Guid userId, string email, string role)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_s.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Role, role),
        };
        var expires = DateTime.UtcNow.AddMinutes(_s.AccessMinutes);
        var token = new JwtSecurityToken(_s.Issuer, _s.Audience, claims, expires: expires, signingCredentials: creds);
        var access = new JwtSecurityTokenHandler().WriteToken(token);
        var refresh = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        return (access, refresh, expires);
    }
}

public sealed class JwtSettings
{
    public string Key { get; init; } = default!;
    public string Issuer { get; init; } = default!;
    public string Audience { get; init; } = default!;
    public int AccessMinutes { get; init; } = 60;
}
