using CollectionMarket_API.Contracts.Repositories;
using CollectionMarket_API.Data;
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
            var categorys = await _context.Orders.ToListAsync();
            return categorys;
        }

        public async Task<Order> GetById(int id)
        {
            var category = await _context.Orders.FindAsync(id);
            return category;
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
    }
}
