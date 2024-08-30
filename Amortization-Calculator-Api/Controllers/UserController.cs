using Amortization_Calculator_Api.Services.users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Amortization_Calculator_Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController: ControllerBase
    {

        private readonly UserServices userServices;

        public UserController(UserServices userServices)
        {
            this.userServices = userServices;
        }


        
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var userListDto = await userServices.GetUsers();

            if (userListDto == null || userListDto.Users.Count == 0)
            {
                return NotFound("No users found.");
            }

            return Ok(userListDto);


        }



        [HttpPost]
        public async Task<IActionResult> ChangeActive()
        {
            await userServices.ChangeActive(); // Await the async method
            return Ok("All users are deactivated.");
        }


    }
}
