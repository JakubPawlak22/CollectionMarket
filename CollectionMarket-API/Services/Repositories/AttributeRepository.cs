using CollectionMarket_API.Contracts.Repositories;
using CollectionMarket_API.Data;
using CollectionMarket_UI.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Services.Repositories
{
    public class AttributeRepository : IAttributeRepository
    {
        private readonly ApplicationDbContext _context;
        public AttributeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Data.Attribute>> GetAll()
        {
            var attributes = await _context.Attributes.ToListAsync();
            return attributes;
        }

        public async Task<Data.Attribute> GetById(int id)
        {
            var attribute = await _context.Attributes.FindAsync(id);
            return attribute;
        }

        public async Task<bool> Create(Data.Attribute entity)
        {
            await _context.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Update(Data.Attribute entity)
        {
            _context.Update(entity);
            return await Save();
        }

        public async Task<bool> Delete(Data.Attribute entity)
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
            var attribute = await _context.Attributes.FindAsync(id);
            return attribute != null;
        }

        public async Task<IList<Data.Attribute>> GetFiltered(AttributeFilters filters)
        {
            var query = _context.Attributes.AsQueryable();
            if (filters != null)
            {
                if (filters.CategoryId != null)
                    query = query
                        .Where(x => x.CategoryAttributes
                            .Where(att => att.CategoryId == filters.CategoryId)
                            .Any());
            }

            var attributes = await query
                .ToListAsync();
            return attributes;
        }
    }
}
