using Amortization_Calculator_Api.Dtos;
using Amortization_Calculator_Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Amortization_Calculator_Api.Services.users
{
    public class UserServices
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserServices(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserListDto> GetUsers()
        {
            var users = await _userManager.Users
            .Select(user => new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                gender = user.gender,
                userType = user.userType,
                phoneNumber = user.PhoneNumber,
                isActivated = user.isActivated,
                usageLease = user.usageLease
            })
            .ToListAsync();

            if (users == null)
            {
                return null;
            }

            return new UserListDto
            {
                Count = users.Count,
                Users = users
            };
        }
    }
}
