using CleanArchMvc.Application.DTOs;


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
