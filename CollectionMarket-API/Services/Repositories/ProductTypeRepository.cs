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
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IList<ProductType>> GetAll()
        {
            var products = await _context.ProductTypes
                .Include(x => x.Category)
                .Include(x => x.AttributeValues)
                .ThenInclude(val => val.Attribute)
                .ToListAsync();
            return products;
        }

        public async Task<IList<ProductType>> GetFiltered(ProductTypeFilters filters)
        {
            var query = _context.ProductTypes.AsQueryable()
                .Include(x => x.Category)
                .Include(x => x.AttributeValues)
                .ThenInclude(val => val.Attribute);
            var products = await query
                .ToListAsync();
            return products;
        }

        public async Task<ProductType> GetById(int id)
        {
            var product = await _context.ProductTypes
                .Include(x => x.Category)
                .Include(x => x.AttributeValues)
                .ThenInclude(val => val.Attribute)
                .SingleOrDefaultAsync(x => x.Id == id);
            return product;
        }

        public async Task<bool> Create(ProductType entity)
        {
            await _context.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Update(ProductType entity)
        {
            var product = await _context.ProductTypes
                .Include(x => x.AttributeValues)
                .SingleOrDefaultAsync(x => x.Id == entity.Id);
            _context.Entry(product).CurrentValues.SetValues(entity);
            var attributeValues = product.AttributeValues.ToList();
            foreach (var oldVal in attributeValues)
            {
                _context.Remove(oldVal);
            }
            foreach (var value in entity.AttributeValues)
            {
                product.AttributeValues.Add(value);
            }
            return await Save();
        }

        public async Task<bool> Delete(ProductType entity)
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
            var product = await _context.ProductTypes.FindAsync(id);
            return product != null;
        }
    }
}
