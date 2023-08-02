using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        //public async Task Add(CategoryDTO categoryDTO)
        public async Task<CategoryDto> Add(CategoryDto categoryDTO)
        {
            var categoryEntity = _mapper.Map<Category>(categoryDTO); 
            var x = await _categoryRepository.CreateAsync(categoryEntity);
            return _mapper.Map<CategoryDto>(x);
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            
            var categoryEntity = await _categoryRepository.GetCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categoryEntity);
            //throw new NotImplementedException();
        }

        public async Task<CategoryDto> GetCategoryById(int id)
        {
            var categoryEntity = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryDto>(categoryEntity);
            //throw new NotImplementedException();
        }

        public async Task Remove(int id)
        {
            var categoryEntity = _categoryRepository.GetByIdAsync(id).Result;
            await _categoryRepository.RemoveAsync(categoryEntity);
            //throw new NotImplementedException();
        }

        public async Task Update(CategoryDto categoryDTO)
        {
            var categoryEntity = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.UpdateAsync(categoryEntity);
            //throw new NotImplementedException();
        }
    }
}
