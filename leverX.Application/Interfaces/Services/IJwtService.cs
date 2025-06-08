using System.Security.Claims;
using leverX.Domain.Entities;

namespace leverX.Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        ClaimsPrincipal ValidateToken(string token);
        DateTime GetTokenExpiry();
    }
}
