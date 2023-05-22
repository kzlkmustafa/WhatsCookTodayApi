using Microsoft.AspNetCore.Mvc;
using WhatsCookTodayApi.Services.Abstracts;

namespace WhatsCookTodayApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet("GetUserInfo")]
        public async Task<IActionResult> GetUser(int UserId)
        {
            var user = await _userService.GetById(UserId);
            return Ok(user);
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int UserId)
        {
            await _userService.Delete(UserId);
            return Ok();
        }

        //[HttpPut("EditUserInfo")]
        //public async Task<IActionResult> UpdateUser(string UserName)
        //{
        //    return Ok();
        //}
    }
}
