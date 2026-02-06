using System.Security.Claims;

namespace Application.Security;

public interface ITokenService
{
    string GenerateToken(List<Claim> claims);
}
