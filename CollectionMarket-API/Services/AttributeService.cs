using AutoMapper;
using CollectionMarket_API.Contracts;
using CollectionMarket_API.Contracts.Repositories;
using CollectionMarket_API.DTOs;
using CollectionMarket_API.Models;
using CollectionMarket_UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Services
{
    public class AttributeService : IAttributeService
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly IMapper _mapper;

        public AttributeService(IAttributeRepository attributeRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _attributeRepository = attributeRepository;
        }

        public async Task<CreateObjectResult> Create(AttributeCreateDTO attributeDTO)
        {
            var attribute = _mapper.Map<Data.Attribute>(attributeDTO);
            var isSuccess = await _attributeRepository.Create(attribute);
            return new CreateObjectResult(isSuccess, attribute.Id);
        }

        public async Task<bool> Delete(int id)
        {
            var attribute = await _attributeRepository.GetById(id);
            var isSuccess = await _attributeRepository.Delete(attribute);
            return isSuccess;
        }

        public async Task<AttributeDTO> Get(int id)
        {
            var attribute = await _attributeRepository.GetById(id);
            var dto = _mapper.Map<AttributeDTO>(attribute);
            return dto;
        }

        public async Task<IList<AttributeDTO>> GetFiltered(AttributeFilters filters)
        {
            var attribute = await _attributeRepository.GetFiltered(filters);
            var dtos = _mapper.Map<IList<AttributeDTO>>(attribute);
            return dtos;
        }

        public async Task<bool> Exists(int id)
        {
            var exists = await _attributeRepository.Exists(id);
            return exists;
        }

        public async Task<bool> Update(AttributeUpdateDTO attributeDTO)
        {
            var attribute = _mapper.Map<Data.Attribute>(attributeDTO);
            var isSuccess = await _attributeRepository.Update(attribute);
            return isSuccess;
        }
    }
}
