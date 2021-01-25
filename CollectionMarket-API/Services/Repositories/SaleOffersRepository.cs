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
    public class SaleOffersRepository : ISaleOffersRepository
    {
        private readonly ApplicationDbContext _context;
        public SaleOffersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<SaleOffer>> GetAll()
        {
            var SaleOffers = await _context.SaleOffers
                .Where(x => x.Order == null)
                .Include(x => x.Seller)
                .Include(x => x.ProductType)
                .ToListAsync();
            return SaleOffers;
        }

        public async Task<SaleOffer> GetById(int id)
        {
            var attribute = await _context.SaleOffers
                .Include(x => x.Seller)
                .FirstOrDefaultAsync(x => x.Id == id);
            return attribute;
        }

        public async Task<bool> Create(SaleOffer entity)
        {
            await _context.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Update(SaleOffer entity)
        {
            _context.Update(entity);
            return await Save();
        }

        public async Task<bool> Delete(SaleOffer entity)
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
            var attribute = await _context.SaleOffers.FindAsync(id);
            return attribute != null;
        }

        public async Task<IList<SaleOffer>> GetFiltered(SaleOfferFilters filters)
        {
            var query = _context.SaleOffers
                .Where(x => x.Order == null)
                .AsQueryable();
            if (filters != null)
            {
                if (filters.ProductTypeId != null)
                    query = query.Where(x => x.ProductTypeId == filters.ProductTypeId);
                if (!string.IsNullOrEmpty(filters.SellerUsername))
                    query = query.Where(x => x.Seller.UserName.Equals(filters.SellerUsername));
            }

            var saleOffers = await query
                .Include(x => x.Seller)
                .ToListAsync();
            return saleOffers;
        }
    }
}
