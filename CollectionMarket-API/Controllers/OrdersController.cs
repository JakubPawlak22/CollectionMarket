﻿using CollectionMarket_API.Contracts;
using CollectionMarket_API.DTOs;
using Common.Enums;
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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILoggerService _logger;

        public OrdersController(IOrderService orderService,
            ILoggerService logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        [Route("soldorders")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLoggedUserSoldOrders()
        {
            try
            {
                var orders = await _orderService.GetSoldBy(RetrieveLoggedUserName());
                return Ok(orders);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("boughtorders")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLoggedUserBoughtOrders()
        {
            try
            {
                var orders = await _orderService.GetBoughtBy(RetrieveLoggedUserName());
                return Ok(orders);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("cart")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLoggedUserCart()
        {
            try
            {
                var orders = await _orderService.GetCart(RetrieveLoggedUserName());
                return Ok(orders);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id < 1)
                    return BadRequest();
                if (!await IsOrderSeller(id) && !await IsOrderBuyer(id))
                    return Unauthorized();
                var order = await _orderService.Get(id);
                return Ok(order);

            }
            catch (Exception e)
            {
                _logger.LogException(e);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("makeorder/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MakeOrder(int id)
        {
            try
            {
                if (id < 1)
                    return BadRequest();
                if (!await IsOrderBuyer(id))
                    return Unauthorized();
                var isSuccess = await _orderService.SetAsOrdered(id);
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

        [HttpPost]
        [Authorize]
        [Route("sent/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SetAsSent(int id)
        {
            try
            {
                if (id < 1)
                    return BadRequest();
                if (!await IsOrderSeller(id))
                    return Unauthorized();
                var isSuccess = await _orderService.SetAsSent(id);
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

        [HttpPost]
        [Authorize]
        [Route("lost/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SetAsLost(int id)
        {
            try
            {
                if (id < 1)
                    return BadRequest();
                if (!await IsOrderBuyer(id))
                    return Unauthorized();
                var isSuccess = await _orderService.SetAsLost(id);
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

        [HttpPost]
        [Authorize]
        [Route("delivered/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SetAsDelivered(int id)
        {
            try
            {
                if (id < 1)
                    return BadRequest();
                if (!await IsOrderBuyer(id))
                    return Unauthorized();
                var isSuccess = await _orderService.SetAsDelivered(id);
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

        [HttpPost]
        [Authorize]
        [Route("evaluation/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddEvaluation(int id, [FromBody] EvaluationDTO evaluation)
        {
            try
            {
                if (id < 1)
                    return BadRequest();
                if (!await IsOrderBuyer(id))
                    return Unauthorized();
                var isSuccess = await _orderService.AddEvaluation(id, evaluation, RetrieveLoggedUserName());
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

        private async Task<bool> IsOrderSeller(int orderId)
        {
            var loggedUserName = RetrieveLoggedUserName();
            var isLoggedUserOrderSeller = await _orderService.IsOrderSeller(orderId, loggedUserName);
            return isLoggedUserOrderSeller;
        }

        private async Task<bool> IsOrderBuyer(int orderId)
        {
            var loggedUserName = RetrieveLoggedUserName();
            var isLoggedUserOrderBuyer = await _orderService.IsOrderBuyer(orderId, loggedUserName);
            return isLoggedUserOrderBuyer;
        }

        private string RetrieveLoggedUserName()
        {
            var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return username;
        }
    }
}
