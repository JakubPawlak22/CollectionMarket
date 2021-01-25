using CollectionMarket_API.Contracts;
using CollectionMarket_API.Contracts.ModelFactories;
using CollectionMarket_API.Contracts.Repositories;
using CollectionMarket_API.Data;
using CollectionMarket_API.DTOs;
using Common.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderModelFactory _orderModelFactory;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly ISaleOffersRepository _saleOfferRepository;

        public OrderService(IOrderModelFactory orderModelFactory,
            IUserService userService,
            ISaleOffersRepository saleOfferRepository,
            UserManager<User> userManager,
            IOrderRepository orderRepository)
        {
            _orderModelFactory = orderModelFactory;
            _orderRepository = orderRepository;
            _userService = userService;
            _saleOfferRepository = saleOfferRepository;
            _userManager = userManager;
        }

        public async Task<bool> AddEvaluation(int id, EvaluationDTO evaluation, string username)
        {
            var order = await _orderRepository.GetById(id);
            if ((OrderState)order.OrderState == OrderState.Delivered)
            {
                order.Evaluation = evaluation.Evaluation;
                order.EvaluationDescription = evaluation.EvaluationDescription;
                var isSuccess = await _orderRepository.Update(order);
                return isSuccess;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AddToCart(int saleOfferId, string buyerUsername)
        {
            var saleOffer = await _saleOfferRepository.GetById(saleOfferId);
            var buyer = await _userManager.FindByNameAsync(buyerUsername);
            bool isSuccess;
            var order = await _orderRepository.GetOrderFromCart(saleOffer.Seller, buyer);
            if (order == null)
            {
                order = _orderModelFactory.CreateNewOrder(saleOffer, buyer);
                isSuccess = await _orderRepository.Create(order);
            }
            else
            {
                _orderModelFactory.AddToOrder(saleOffer, order);
                isSuccess = await _orderRepository.Update(order);
            }
            return isSuccess;
        }

        public async Task<OrderDTO> Get(int id)
        {
            var order = await _orderRepository.GetById(id);
            var dto = _orderModelFactory.CreateOrderDTO(order);
            return dto;
        }

        public async Task<IList<OrderDTO>> GetBoughtBy(string username)
        {
            var orders = await _orderRepository.GetBoughtBy(username);
            var dtos = orders
                .Select(x => _orderModelFactory.CreateOrderDTO(x))
                .ToList();
            return dtos;
        }

        public async Task<IList<OrderDTO>> GetCart(string username)
        {
            var orders = await _orderRepository.GetCart(username);
            var dtos = orders
                .Select(x => _orderModelFactory.CreateOrderDTO(x))
                .ToList();
            return dtos;
        }

        public async Task<IList<OrderDTO>> GetSoldBy(string username)
        {
            var orders = await _orderRepository.GetSoldBy(username);
            var dtos = orders
                .Select(x => _orderModelFactory.CreateOrderDTO(x))
                .ToList();
            return dtos;
        }

        public async Task<bool> IsOrderBuyer(int orderId, string loggedUserName)
        {
            var order = await _orderRepository.GetById(orderId);
            return order.Buyer.UserName.Equals(loggedUserName);
        }

        public async Task<bool> IsOrderSeller(int orderId, string loggedUserName)
        {
            var order = await _orderRepository.GetById(orderId);
            return order.SaleOffers.FirstOrDefault().Seller.UserName.Equals(loggedUserName);
        }

        public async Task<bool> RemoveFromCart(int saleOfferId, string buyerUsername)
        {
            var saleOffer = await _saleOfferRepository.GetById(saleOfferId);
            var buyer = await _userManager.FindByNameAsync(buyerUsername);
            bool isSuccess;
            var order = await _orderRepository.GetOrderFromCart(saleOffer.Seller, buyer);
            if (order == null)
            {
                isSuccess = false;
            }
            else
            {
                _orderModelFactory.RemoveFromCart(saleOffer, order);
                if (order.SaleOffers.Any())
                    isSuccess = await _orderRepository.Update(order);
                else
                    isSuccess = await _orderRepository.Delete(order);
            }
            return isSuccess;
        }

        public async Task<bool> SetAsDelivered(int id)
        {
            var order = await _orderRepository.GetById(id);
            if (order.OrderState != (int)OrderState.Ordered && order.OrderState != (int)OrderState.Lost)
                return false;
            order.OrderState = (int)OrderState.Delivered;
            var isSuccess = await _orderRepository.Update(order);
            return isSuccess;
        }

        public async Task<bool> SetAsLost(int id)
        {
            var order = await _orderRepository.GetById(id);
            if (order.OrderState != (int)OrderState.Sent)
                return false;
            order.OrderState = (int)OrderState.Lost;
            var isSuccess = await _orderRepository.Update(order);
            return isSuccess;
        }

        public async Task<bool> SetAsOrdered(int id)
        {
            var order = await _orderRepository.GetById(id);
            if (!_userService.HasEnoughMoney(order.Buyer, order.Price))
                return false;
            if (order.OrderState != (int)OrderState.InCart)
                return false;
            order.OrderState = (int)OrderState.Ordered;
            _orderModelFactory.AddAddressInformations(order);
            var isSuccess = await _orderRepository.Update(order);
            if (isSuccess)
            {
                var isTransactionSuccessful = await _userService.SendMoney(order);
                return isTransactionSuccessful;
            }
            return false;
        }

        public async Task<bool> SetAsSent(int id)
        {
            var order = await _orderRepository.GetById(id);
            if (order.OrderState != (int)OrderState.Ordered)
                return false;
            order.OrderState = (int)OrderState.Sent;
            var isSuccess = await _orderRepository.Update(order);
            return isSuccess;
        }
    }
}
