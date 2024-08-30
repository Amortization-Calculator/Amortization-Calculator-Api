using Amortization_Calculator_Api.Dtos;
using Amortization_Calculator_Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;

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

        public async Task ChangeActive()
        {
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                if (user.UserName != "admin")
                {
                    user.isActivated = false;
                }
        
                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    Console.WriteLine($"Failed to update user {user.Id}: {result.Errors.FirstOrDefault()?.Description}");
                }
            }
        }


    }
}
