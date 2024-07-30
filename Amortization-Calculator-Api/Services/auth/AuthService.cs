using Amortization_Calculator_Api.Config;
using Amortization_Calculator_Api.Dtos;
using Amortization_Calculator_Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Amortization_Calculator_Api.Services.auth
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public AuthService(UserManager<ApplicationUser> userManager, JwtOptions jwtOptions)
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions;
        }




        public async Task<JwtSecurityToken> generateToken(ApplicationUser user)
        {
            var tokenHandelr = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Expires = DateTime.Now.AddDays(_jwtOptions.DurationInDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key)), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Email,user.Email),
                    new(ClaimTypes.Name,user.UserName),
                    new(ClaimTypes.Role,user.userType.ToString()),
                    new(ClaimTypes.NameIdentifier,user.Id.ToString())
                })
            };

            var securityToken = tokenHandelr.CreateToken(tokenDescriptor);

            return securityToken as JwtSecurityToken;
        }

        public async Task<AuthResponse> LoginUserAsync(LoginDto loginDto)
        {

            //check if user exists
            var user = await _userManager.FindByNameAsync(loginDto.userName);

            if (user is null || !await _userManager.CheckPasswordAsync(user, loginDto.password))
            {
                return null;
            }

            //generate token
            var token = await generateToken(user);

            return new AuthResponse
            {
                Message = "Login successful",
                token = new JwtSecurityTokenHandler().WriteToken(token),
                email = user.Email,
                userName = user.UserName,
                gender = user.gender,
                userType = user.userType,
                isAuthSuccessful = true,
                expireDate = token.ValidTo

            };

        }

        public async Task<AuthResponse> RegisterUserAsync(RegisterDto registerDto)
        {
            //check if user exists by email
            var userCheckedByEmail = await _userManager.FindByEmailAsync(registerDto.email);

            if (userCheckedByEmail!=null)
            {
                return new AuthResponse { Message = "Email already in use" , isAuthSuccessful=false };
            }

            //check if user exists by username

            var userCheckedByUserName = await _userManager.FindByNameAsync(registerDto.userName);

            if (userCheckedByUserName!=null)
            {
                return new AuthResponse { Message = "Username already in use"  , isAuthSuccessful = false };
            }

            //create user
            var user = new ApplicationUser
            {
                UserName = registerDto.userName,
                Email = registerDto.email,
                PhoneNumber = registerDto.phoneNumber,
                gender = registerDto.gender,
                userType = registerDto.userType
            };

            var result = await _userManager.CreateAsync(user, registerDto.password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthResponse { Message = errors };
            }


            //generate token
            var token = await generateToken(user);

            return new AuthResponse
            {
                Message = "User created successfully",
                token = new JwtSecurityTokenHandler().WriteToken(token),
                isAuthSuccessful = true,
                email = user.Email,
                userName = user.UserName,
                gender = user.gender,
                userType = user.userType,
                expireDate = token.ValidTo
            };


        }
    }
}
