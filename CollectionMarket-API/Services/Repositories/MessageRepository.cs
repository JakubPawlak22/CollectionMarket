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
            if (filters != null)
            {
                if (!string.IsNullOrEmpty(filters.FirstUserName) && !string.IsNullOrEmpty(filters.SecondUserName))
                    query = query.Where(x =>
                        (x.Receiver.UserName.Equals(filters.FirstUserName)
                            && x.Sender.UserName.Equals(filters.SecondUserName))
                        || (x.Sender.UserName.Equals(filters.FirstUserName)
                            && x.Receiver.UserName.Equals(filters.SecondUserName)));

            }

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

        public async Task<bool> Exists(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            return message != null;
        }

        public async Task<IList<Message>> GetLastMessages(string loggedUserName)
        {
            var users = new List<string>();
            var senders = _context.Messages
                 .Where(x => x.Receiver.UserName.Equals(loggedUserName))
                 .Select(x => x.Sender.UserName)
                 .Distinct()
                 .ToList();
            var receivers = _context.Messages
                 .Where(x => x.Sender.UserName.Equals(loggedUserName))
                 .Select(x => x.Receiver.UserName)
                 .Distinct()
                 .ToList();
            users.AddRange(senders);
            users.AddRange(receivers);
            users = users.Distinct().ToList();

            List<Message> messages = new List<Message>();
            foreach (var user in users)
            {
                var msg = await _context.Messages
                    .Where(x =>
                    (x.Receiver.UserName.Equals(loggedUserName)
                    && x.Sender.UserName.Equals(user)) ||
                    (x.Sender.UserName.Equals(loggedUserName)
                    && x.Receiver.UserName.Equals(user)))
                    .OrderBy(x => x.Date)
                    .Include(x => x.Sender)
                    .Include(x => x.Receiver)
                    .LastOrDefaultAsync();
                messages.Add(msg);
            }
            return messages;
        }
    }
}
