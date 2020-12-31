using CollectionMarket_API.Contracts;
using CollectionMarket_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IMessageService _messageService;
        private readonly ILoggerService _logger;
        public MessagesController(IMessageService messageService,
            ILoggerService logger)
        {
            _messageService = messageService;
            _logger = logger;
        }

        /// <summary>
        /// Get all Messages between two Users
        /// </summary>
        /// <param name="filtersDTO">Filters</param>
        /// <returns>All Messages between two Users</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMessages([FromBody] MessageFiltersDTO filtersDTO)
        {
            try
            {
                var messages = await _messageService.GetConversation(filtersDTO);
                return Ok(messages);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Create a Message
        /// </summary>
        /// <param name="createDTO"></param>
        /// <returns>A Message's ID</returns>
        [HttpPost]
        [Authorize(Roles = "Administrator, Client")]
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
                var result = await _messageService.Create(createDTO);
                if (!result.IsSuccess)
                    return StatusCode(500, "An internal server errror was occured.");
                return Created("Create", result.ObjectId);
            }
            catch (Exception e)
            {
                return StatusCode(500, "An internal server errror was occured.");
            }
        }
    }
}
