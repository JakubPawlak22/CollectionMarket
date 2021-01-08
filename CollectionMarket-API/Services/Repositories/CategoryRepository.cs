using CollectionMarket_API.Contracts.Repositories;
using CollectionMarket_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Services.Repositories
{
    public class CategoryRepository: ICategoryRepository
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
            var category = await _context.Categories.FindAsync(id);
            return category;
        }

        public async Task<bool> Create(Category entity)
        {
            await _context.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Update(Category entity)
        {
            _context.Update(entity);
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
