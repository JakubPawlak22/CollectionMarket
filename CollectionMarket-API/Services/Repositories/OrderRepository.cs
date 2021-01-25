using CollectionMarket_API.Contracts.Repositories;
using CollectionMarket_API.Data;
using CollectionMarket_API.DTOs;
using Common.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Services.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Order>> GetAll()
        {
            var orders = await _context.Orders
                .Include(x => x.Buyer)
                .Include(x => x.SaleOffers)
                .ThenInclude(x => x.Seller)
                .Include(x => x.SaleOffers)
                .ThenInclude(x => x.ProductType)
                .ToListAsync();
            return orders;
        }

        public async Task<Order> GetById(int id)
        {
            var order = await _context.Orders
                .Include(x => x.Buyer)
                .Include(x => x.SaleOffers)
                .ThenInclude(x => x.Seller)
                .Include(x => x.SaleOffers)
                .ThenInclude(x => x.ProductType)
                .FirstOrDefaultAsync(x => x.Id == id);
            return order;
        }

        public async Task<bool> Create(Order entity)
        {
            await _context.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Update(Order entity)
        {
            _context.Update(entity);
            return await Save();
        }

        public async Task<bool> Delete(Order entity)
        {
            _context.Remove(entity);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Exists(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            return order != null;
        }

        public async Task<IList<Order>> GetSoldBy(string username)
        {
            var orders = await _context.Orders
                .Where(x => x.SaleOffers.All(x => x.Seller.UserName.Equals(username)))
                .Where(x => x.OrderState != (int)OrderState.InCart
                    && x.SaleOffers.Any())
                .Include(x => x.Buyer)
                .Include(x => x.SaleOffers)
                .ThenInclude(x => x.Seller)
                .Include(x => x.SaleOffers)
                .ThenInclude(x => x.ProductType)
                .ToListAsync();
            return orders;
        }

        public async Task<IList<Order>> GetBoughtBy(string username)
        {
            var orders = await _context.Orders
                .Where(x => x.Buyer.UserName.Equals(username))
                .Where(x => x.OrderState != (int)OrderState.InCart
                    && x.SaleOffers.Any())
                .Include(x => x.Buyer)
                .Include(x => x.SaleOffers)
                .ThenInclude(x => x.Seller)
                .Include(x => x.SaleOffers)
                .ThenInclude(x => x.ProductType)
                .ToListAsync();
            return orders;
        }

        public async Task<IList<Order>> GetCart(string username)
        {
            var orders = await _context.Orders
                .Where(x => x.Buyer.UserName.Equals(username)
                    && x.OrderState == (int)OrderState.InCart
                    && x.SaleOffers.Any())
                .Include(x => x.Buyer)
                .Include(x => x.SaleOffers)
                .ThenInclude(x => x.Seller)
                .Include(x => x.SaleOffers)
                .ThenInclude(x => x.ProductType)
                .ToListAsync();
            return orders;
        }

        public async Task<Order> GetOrderFromCart(User seller, User buyer)
        {
            return await _context.Orders
                .Where(x => x.OrderState == (int)OrderState.InCart
                    && x.SaleOffers.Any()
                    && x.SaleOffers.FirstOrDefault().Seller == seller
                    && x.Buyer == buyer)
                .FirstOrDefaultAsync();
        }
    }
}
