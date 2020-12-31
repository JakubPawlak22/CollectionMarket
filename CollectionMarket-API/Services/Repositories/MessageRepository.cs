using CollectionMarket_API.Contracts.Repositories;
using CollectionMarket_API.Data;
using CollectionMarket_API.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Services.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;
        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Message>> GetAll()
        {
            var messages = await _context.Messages.Include(x => x.Receiver).Include(x => x.Sender).ToListAsync();
            return messages;
        }

        public async Task<IList<Message>> GetFiltered(MessageFilters filters)
        {
            var query = _context.Messages.AsQueryable();

            if (!string.IsNullOrEmpty(filters.FirstUserId) && !string.IsNullOrEmpty(filters.SecondUserId))
                query = query.Where(x =>
                    (x.ReceiverId.Equals(filters.FirstUserId)
                        && x.SenderId.Equals(filters.SecondUserId))
                    || (x.SenderId.Equals(filters.FirstUserId)
                        && x.ReceiverId.Equals(filters.SecondUserId)));

            var messages = await query
                .Include(x => x.Receiver)
                .Include(x => x.Sender)
                .OrderBy(x => x.Date)
                .ToListAsync();
            return messages;
        }

        public async Task<Message> GetById(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            return message;
        }

        public async Task<bool> Create(Message entity)
        {
            await _context.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Update(Message entity)
        {
            _context.Update(entity);
            return await Save();
        }

        public async Task<bool> Delete(Message entity)
        {
            _context.Remove(entity);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
    }
}
