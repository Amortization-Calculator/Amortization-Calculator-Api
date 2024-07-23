using Amortization_Calculator_Api.Dtos;
using Amortization_Calculator_Api.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Amortization_Calculator_Api.Services.auth
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterUserAsync(RegisterDto registerDto);

        Task<AuthResponse> LoginUserAsync(LoginDto loginDto);

        Task<JwtSecurityToken> generateToken(ApplicationUser user);
    }
}
