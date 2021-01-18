using CollectionMarket_API.Contracts;
using CollectionMarket_API.DTOs;
using CollectionMarket_API.Filters;
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
    [Route("api/[controller]")]
    [ApiController]
    public class SaleOffersController : ControllerBase
    {
        private readonly ISaleOffersService _saleOffersService;
        private readonly ILoggerService _logger;

        public SaleOffersController(ISaleOffersService saleOffersService,
            ILoggerService logger)
        {
            _saleOffersService = saleOffersService;
            _logger = logger;
        }

        /// <summary>
        /// Get all Sale Offers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSaleOffers([FromBody] SaleOfferFilters filters)
        {
            try
            {
                var offers = await _saleOffersService.GetFiltered(filters);
                return Ok(offers);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Gets Sale Offer by id
        /// </summary>
        /// <param name="id">Sale Offer's ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSaleOffer(int id)
        {
            try
            {
                if (id < 1)
                    return BadRequest();
                if (!await _saleOffersService.Exists(id))
                    return NotFound();
                var offer = await _saleOffersService.Get(id);
                return Ok(offer);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Creates new Sale Offer
        /// </summary>
        /// <param name="offer">Sale Offer's informations</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] SaleOfferCreateDTO offer)
        {
            try
            {
                if (offer == null)
                    return BadRequest();
                if (!ModelState.IsValid)
                    return BadRequest();
                var name = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var result = await _saleOffersService.Create(offer, name);
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
        /// Updates Sale Offer
        /// </summary>
        /// <param name="id">SaleOffer's ID</param>
        /// <param name="offer">SaleOffer's informations</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] SaleOfferUpdateDTO offer)
        {
            try
            {
                if (! await IsOfferOwner(offer.Id))
                    return BadRequest();
                if (offer == null || id < 1 || id != offer.Id)
                    return BadRequest();
                if (!ModelState.IsValid)
                    return BadRequest();
                var isSuccess = await _saleOffersService.Update(offer);
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
        /// Removes SaleOffer by id
        /// </summary>
        /// <param name="id">SaleOffer's ID</param>
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
                if (!await _saleOffersService.Exists(id))
                    return NotFound();
                if (!await IsOfferOwner(id))
                    return BadRequest();
                var isSuccess = await _saleOffersService.Delete(id);
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

        private async Task<bool> IsOfferOwner(int offerId)
        {
            var loggedUserName = RetrieveLoggedUserName();
            var isLoggedUserOfferOwner = await _saleOffersService.IsOfferOwner(offerId, loggedUserName);
            return isLoggedUserOfferOwner;
        }

        private string RetrieveLoggedUserName()
        {
            var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return username;
        }
    }
}
