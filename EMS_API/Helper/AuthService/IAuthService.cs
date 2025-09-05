using EMS.BLL.DTOs.User;
using System.Security.Claims;

namespace EMS_API.Helper
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(RegisterDto request);
        Task<AuthResult> LoginAsync(LoginDto request);
        Task<AuthResult> RefreshAccessTokenAsync(string refreshToken);
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();

    }





}
