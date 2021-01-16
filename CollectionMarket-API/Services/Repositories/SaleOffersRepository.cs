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
    public class SaleOffersRepository: ISaleOffersRepository
    {
        private readonly ApplicationDbContext _context;
        public SaleOffersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<SaleOffer>> GetAll()
        {
            var SaleOffers = await _context.SaleOffers.ToListAsync();
            return SaleOffers;
        }

        public async Task<SaleOffer> GetById(int id)
        {
            var attribute = await _context.SaleOffers.FindAsync(id);
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
            var query = _context.SaleOffers.AsQueryable();
            if (filters != null)
            {

            }

            var saleOffers = await query
                .ToListAsync();
            return saleOffers;
        }
    }
}
