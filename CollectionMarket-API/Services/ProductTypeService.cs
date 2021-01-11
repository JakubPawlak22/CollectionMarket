using AutoMapper;
using CollectionMarket_API.Contracts;
using CollectionMarket_API.Contracts.ModelFactories;
using CollectionMarket_API.Contracts.Repositories;
using CollectionMarket_API.Contracts.Validators;
using CollectionMarket_API.Data;
using CollectionMarket_API.DTOs;
using CollectionMarket_API.Filters;
using CollectionMarket_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Services
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IProductTypeModelFactory _productTypeModelFactory;
        private readonly IProductTypeWithAttributesValidator _validator;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        public ProductTypeService(IProductTypeRepository productTypeRepository,
            IProductTypeModelFactory productTypeModelFactory,
            IProductTypeWithAttributesValidator validator,
            IMapper mapper,
            ILoggerService logger)
        {
            _productTypeRepository = productTypeRepository;
            _productTypeModelFactory = productTypeModelFactory;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }
        public async Task<CreateObjectResult> Create(ProductTypeCreateDTO productTypeDTO)
        {
            var product = _productTypeModelFactory.CreateEntity(productTypeDTO);
            var result = await _validator.Valid(product);
            if (result.IsValid)
            {
                var isSuccess = await _productTypeRepository.Create(product);
                return new CreateObjectResult(isSuccess, product.Id);
            }
            else
            {
                _logger.LogError($"Create Product Type error - Product Type is not valid. Validator message: {result.Message}");
                return new CreateObjectResult(false, null);
            }
        }

        public async Task<bool> Delete(int id)
        {
            var product = await _productTypeRepository.GetById(id);
            var isSuccess = await _productTypeRepository.Delete(product);
            return isSuccess;
        }

        public async Task<bool> Exists(int id)
        {
            var exists = await _productTypeRepository.Exists(id);
            return exists;
        }

        public async Task<ProductTypeDTO> Get(int id)
        {
            var product = await _productTypeRepository.GetById(id);
            var dto = _mapper.Map<ProductTypeDTO>(product);
            return dto;
        }

        public async Task<IList<ProductTypeDTO>> GetFiltered(ProductTypeFilters filters)
        {
            var products = await _productTypeRepository.GetFiltered(filters);
            var dto = _mapper.Map<IList<ProductTypeDTO>>(products);
            return dto;
        }

        public async Task<bool> Update(ProductTypeUpdateDTO productTypeDTO)
        {
            var product = _productTypeModelFactory.CreateEntity(productTypeDTO);
            var result = await _validator.Valid(product);
            if (result.IsValid)
            {
                var isSuccess = await _productTypeRepository.Update(product);
                return isSuccess;
            }
            else
            {
                _logger.LogError($"Update Product Type error - Product Type is not valid. Validator message: {result.Message}");
                return false;
            }
        }
    }
}
