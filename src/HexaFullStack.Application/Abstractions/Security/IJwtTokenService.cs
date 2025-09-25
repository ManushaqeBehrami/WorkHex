namespace HexaFullStack.Application.Abstractions.Security;
public interface IJwtTokenService
{
    (string accessToken, string refreshToken, DateTime expiresUtc) CreateTokens(Guid userId, string email, string role);
}
