using CleanArchMvc.Application.DTOs;


namespace CleanArchMvc.Application.Interfaces
{
    public  interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetById(int id);
        Task<ProductDto> GetProductCategory(int id);
        Task Add(ProductDto productDTO);
        Task Update(ProductDto productDTO);
        Task Remove(int id);
    }
}
