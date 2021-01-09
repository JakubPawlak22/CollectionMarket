using AutoMapper;
using CollectionMarket_API.Contracts;
using CollectionMarket_API.Contracts.Repositories;
using CollectionMarket_API.Data;
using CollectionMarket_API.DTOs;
using CollectionMarket_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<CreateObjectResult> Create(CategoryCreateDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            category.CategoryAttributes = new List<CategoryAttributes>();
            var attributeIds = categoryDTO.Attributes
                .Select(x => x.Id)
                .Distinct();
            foreach (var attributeId in attributeIds)
            {
                category.CategoryAttributes.Add(new CategoryAttributes
                {
                    Category = category,
                    AttributeId = attributeId
                });
            }
            var isSuccess = await _categoryRepository.Create(category);
            return new CreateObjectResult
            {
                ObjectId = category.Id,
                IsSuccess = isSuccess
            };
        }

        public async Task<bool> Delete(int id)
        {
            var category = await _categoryRepository.GetById(id);
            var isSuccess = await _categoryRepository.Delete(category);
            return isSuccess;
        }

        public async Task<CategoryDTO> Get(int id)
        {
            var category = await _categoryRepository.GetById(id);
            var dto = _mapper.Map<CategoryDTO>(category);
            return dto;
        }

        public async Task<IList<CategoryDTO>> GetAll()
        {
            var category = await _categoryRepository.GetAll();
            var dtos = _mapper.Map<IList<CategoryDTO>>(category);
            return dtos;
        }

        public async Task<bool> Exists(int id)
        {
            var exists = await _categoryRepository.Exists(id);
            return exists;
        }

        public async Task<bool> Update(CategoryUpdateDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            category.CategoryAttributes = new List<CategoryAttributes>();
            var attributeIds = categoryDTO.Attributes
                .Select(x => x.Id)
                .Distinct();
            foreach (var attributeId in attributeIds)
            {
                category.CategoryAttributes.Add(new CategoryAttributes
                {
                    Category = category,
                    AttributeId = attributeId
                });
            }
            var isSuccess = await _categoryRepository.Update(category);
            return isSuccess;
        }
    }
}
