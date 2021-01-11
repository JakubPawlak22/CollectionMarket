using CollectionMarket_API.Contracts;
using CollectionMarket_API.DTOs;
using CollectionMarket_API.Filters;
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
    public class ProductTypesController : ControllerBase
    {
        private readonly IProductTypeService _productTypeService;
        private readonly ILoggerService _logger;

        public ProductTypesController(IProductTypeService productTypeService,
            ILoggerService logger)
        {
            _productTypeService = productTypeService;
            _logger = logger;
        }

        /// <summary>
        /// Get all Product Types
        /// </summary>
        /// <param name="filters">Filters</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductTypes([FromBody] ProductTypeFilters filters)
        {
            try
            {
                var products = await _productTypeService.GetFiltered(filters);
                return Ok(products);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Gets ProductType by id
        /// </summary>
        /// <param name="id">ProductType's ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductType(int id)
        {
            try
            {
                if (id < 1)
                    return BadRequest();
                if (!await _productTypeService.Exists(id))
                    return NotFound();
                var product = await _productTypeService.Get(id);
                return Ok(product);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Creates new ProductType
        /// </summary>
        /// <param name="product">ProductType's informations</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ProductTypeCreateDTO product)
        {
            try
            {
                if (product == null)
                    return BadRequest();
                if (!ModelState.IsValid)
                    return BadRequest();
                var result = await _productTypeService.Create(product);
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
        /// Updates ProductType
        /// </summary>
        /// <param name="id">ProductType's ID</param>
        /// <param name="product">ProductType's informations</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] ProductTypeUpdateDTO product)
        {
            try
            {
                if (product == null || id < 1 || id != product.Id)
                    return BadRequest();
                if (!ModelState.IsValid)
                    return BadRequest();
                var isSuccess = await _productTypeService.Update(product);
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
        /// Removes ProductType by id
        /// </summary>
        /// <param name="id">ProductType's ID</param>
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
                if (!await _productTypeService.Exists(id))
                    return NotFound();
                var isSuccess = await _productTypeService.Delete(id);
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
