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
        private readonly ILoggerService _logger;

        public UsersController(IUserService userService,
            ILoggerService logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="loginDTO">Username and Password</param>
        /// <returns></returns>
        [Route("login")]
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
                _logger.LogException(e);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// User Registration 
        /// </summary>
        /// <param name="userRegisterDTO">Account informations</param>
        /// <returns></returns>
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]UserRegisterDTO userRegisterDTO)
        {
            try
            {
                if (userRegisterDTO == null)
                    return BadRequest(ModelState);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var isSuccess = await _userService.Register(userRegisterDTO);
                if(!isSuccess)
                    return StatusCode(500);
                return Ok();

            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return StatusCode(500);
            }
        }
    }
}
