using CollectionMarket_API.Contracts.Repositories;
using CollectionMarket_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Services.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Category>> GetAll()
        {
            var categorys = await _context.Categories.ToListAsync();
            return categorys;
        }

        public async Task<Category> GetById(int id)
        {
            var category = await _context.Categories
                .Include(x => x.CategoryAttributes)
                .ThenInclude(x=>x.Attribute)
                .SingleOrDefaultAsync(x=>x.Id==id);
            return category;
        }

        public async Task<bool> Create(Category entity)
        {
            await _context.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Update(Category entity)
        {
            var category = await _context.Categories
                .Include(x => x.CategoryAttributes)
                .SingleOrDefaultAsync(x => x.Id == entity.Id);
            _context.Entry(category).CurrentValues.SetValues(entity);
            var attributeMappings = category.CategoryAttributes.ToList();
            foreach (var mapping in attributeMappings)
            {
                var contact = entity.CategoryAttributes
                    .FirstOrDefault(x => x.AttributeId == mapping.AttributeId);
                if (contact == null)
                    _context.Remove(mapping);
            }
            foreach (var mapping in entity.CategoryAttributes)
            {
                if (attributeMappings.All(i => i.AttributeId != mapping.AttributeId))
                {
                    category.CategoryAttributes.Add(mapping);
                }
            }
            return await Save();
        }

        public async Task<bool> Delete(Category entity)
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
            var category = await _context.Categories.FindAsync(id);
            return category != null;
        }
    }
}
