using EcomHub.Domain.Entities;
using System.Security.Claims;

namespace EcomHub.Application.Interfaces;

public interface IJwtService
{
    (string Token, DateTime Expiration) GenerateAccessToken(User user, IList<string> roles);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
