using CollectionMarket_API.Contracts;
using CollectionMarket_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CollectionMarket_API.Controllers
{
    /// <summary>
    /// Endpoint used to interact with Messages in database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class MessagesController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly ILoggerService _logger;
        public MessagesController(IMessageService messageService,
            IUserService userService,
            ILoggerService logger)
        {
            _messageService = messageService;
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Get all Messages between logged User and choosen User
        /// </summary>
        /// <param name="username"></param>
        /// <returns>All Messages between two Users</returns>
        [HttpGet("{username}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetConversation(string username)
        {
            try
            {
                if (!await _userService.Exists(username))
                    return NotFound();
                var loggedUserName = RetrieveLoggedUserName();
                var messages = await _messageService.GetConversation(loggedUserName,username);
                return Ok(messages);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return StatusCode(500);
            }
        }
        
        /// <summary>
        /// Get conversations list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetConversations()
        {
            try
            {
                var loggedUserName = RetrieveLoggedUserName();
                var conversations = await _messageService.GetConversations(loggedUserName);
                return Ok(conversations);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Create a Message
        /// </summary>
        /// <param name="createDTO"></param>
        /// <returns>A Message's ID</returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] MessageCreateDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                    return BadRequest(ModelState);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var loggedUserName = RetrieveLoggedUserName();
                var result = await _messageService.Create(createDTO, loggedUserName);
                if (!result.IsSuccess)
                    return StatusCode(500, "An internal server errror was occured.");
                return Created("Create", result.ObjectId);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return StatusCode(500, "An internal server errror was occured.");
            }
        }

        private string RetrieveLoggedUserName()
        {
            var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return username;
        }
    }
}
