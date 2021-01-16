using CollectionMarket_API.DTOs;
using CollectionMarket_API.Filters;
using CollectionMarket_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Contracts
{
    public interface ISaleOffersService
    {
        Task<CreateObjectResult> Create(SaleOfferCreateDTO saleOfferDTO);
        Task<bool> Update(SaleOfferUpdateDTO saleOfferDTO);
        Task<SaleOfferDTO> Get(int id);
        Task<IList<SaleOfferDTO>> GetFiltered(SaleOfferFilters filters);
        Task<bool> Delete(int id);
        Task<bool> Exists(int id);
    }
}
