using CleanArchMvc.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CleanArchMvc.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategories();
       
        Task<CategoryDto> GetCategoryById(int id);

        Task<CategoryDto> Add(CategoryDto categoryDTO);

        Task Update(CategoryDto categoryDTO);

        Task Remove(int id);

    }
}
