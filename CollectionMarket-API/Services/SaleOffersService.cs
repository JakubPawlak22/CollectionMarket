using AutoMapper;
using CollectionMarket_API.Contracts;
using CollectionMarket_API.Contracts.Repositories;
using CollectionMarket_API.Data;
using CollectionMarket_API.DTOs;
using CollectionMarket_API.Filters;
using CollectionMarket_API.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Services
{
    public class SaleOffersService: ISaleOffersService
    {
        private readonly ISaleOffersRepository _saleOffersRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public SaleOffersService(ISaleOffersRepository saleOffersRepository,
            UserManager<User> userManager,
            IMapper mapper)
        {
            _mapper = mapper;
            _saleOffersRepository = saleOffersRepository;
            _userManager = userManager;
        }

        public async Task<CreateObjectResult> Create(SaleOfferCreateDTO saleOfferDTO, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var saleOffer = _mapper.Map<Data.SaleOffer>(saleOfferDTO);
            saleOffer.Seller = user;
            var isSuccess = await _saleOffersRepository.Create(saleOffer);
            return new CreateObjectResult(isSuccess, saleOffer.Id);
        }

        public async Task<bool> Delete(int id)
        {
            var saleOffer = await _saleOffersRepository.GetById(id);
            var isSuccess = await _saleOffersRepository.Delete(saleOffer);
            return isSuccess;
        }

        public async Task<SaleOfferDTO> Get(int id)
        {
            var saleOffer = await _saleOffersRepository.GetById(id);
            var dto = _mapper.Map<SaleOfferDTO>(saleOffer);
            return dto;
        }

        public async Task<IList<SaleOfferDTO>> GetFiltered(SaleOfferFilters filters)
        {
            var saleOffers = await _saleOffersRepository.GetFiltered(filters);
            var dtos = _mapper.Map<IList<SaleOfferDTO>>(saleOffers);
            return dtos;
        }

        public async Task<bool> Exists(int id)
        {
            var exists = await _saleOffersRepository.Exists(id);
            return exists;
        }

        public async Task<bool> Update(SaleOfferUpdateDTO saleOfferDTO)
        {
            var saleOffer = _mapper.Map<Data.SaleOffer>(saleOfferDTO);
            var isSuccess = await _saleOffersRepository.Update(saleOffer);
            return isSuccess;
        }
    }
}
