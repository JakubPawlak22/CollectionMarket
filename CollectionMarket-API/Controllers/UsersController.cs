using CollectionMarket_API.Contracts;
using CollectionMarket_API.Data;
using CollectionMarket_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="loginDTO">Username and Password</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO loginDTO)
        {
            try
            {
                var result = await _userService.SignIn(loginDTO.Username, loginDTO.Password);
                if (result.Succeeded)
                {
                    var tokenString = await _userService.GenerateJSONWebToken(loginDTO.Username);
                    return Ok(new { token = tokenString });
                }
                return Unauthorized();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
