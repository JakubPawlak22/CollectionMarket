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
    [Route("api/[controller]")]
    [ApiController]
    public class AttributesController : ControllerBase
    {
        private readonly IAttributeService _attributeService;
        private readonly ILoggerService _logger;

        public AttributesController(IAttributeService attributeService,
            ILoggerService logger)
        {
            _attributeService = attributeService;
            _logger = logger;
        }

        /// <summary>
        /// Get all Attributes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAttributes()
        {
            try
            {
                var attributes = await _attributeService.GetAll();
                return Ok(attributes);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Gets Attribute by id
        /// </summary>
        /// <param name="id">Attribute's ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAttribute(int id)
        {
            try
            {
                if (id < 1)
                    return BadRequest();
                if (!await _attributeService.Exists(id))
                    return NotFound();
                var attribute = await _attributeService.Get(id);
                return Ok(attribute);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Creates new Attribute
        /// </summary>
        /// <param name="attribute">Attribute's informations</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AttributeCreateDTO attribute)
        {
            try
            {
                if (attribute == null)
                    return BadRequest();
                if (!ModelState.IsValid)
                    return BadRequest();
                var result = await _attributeService.Create(attribute);
                if (!result.IsSuccess)
                    return StatusCode(500);
                return Created("Create", result.ObjectId);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Updates Attribute
        /// </summary>
        /// <param name="id">Attribute's ID</param>
        /// <param name="attribute">Attribute's informations</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] AttributeUpdateDTO attribute)
        {
            try
            {
                if (attribute == null || id < 1 || id != attribute.Id)
                    return BadRequest();
                if (!ModelState.IsValid)
                    return BadRequest();
                var isSuccess = await _attributeService.Update(attribute);
                if (!isSuccess)
                    return StatusCode(500);
                return StatusCode(204);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Removes Attribute by id
        /// </summary>
        /// <param name="id">Attribute's ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1)
                    return BadRequest();
                if (!await _attributeService.Exists(id))
                    return NotFound();
                var isSuccess = await _attributeService.Delete(id);
                if (!isSuccess)
                    return StatusCode(500);
                return StatusCode(204);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return StatusCode(500);
            }
        }
    }
}
