using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.Services
{
    public class ProductService:IProductService
    {
        private IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ??
                    throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var productsEntity = await _productRepository.GetProductsAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(productsEntity);
            //throw new NotImplementedException();
        }

        public async Task<ProductDto> GetById(int id)
        {
            var productsEntity = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(productsEntity);
            //throw new NotImplementedException();
        }

        public async Task<ProductDto> GetProductCategory(int id)
        {
            var productsEntity = await _productRepository.GetProductCategoryAsync(id);
            return _mapper.Map<ProductDto>(productsEntity);
            //throw new NotImplementedException();
        }

        public async Task Add(ProductDto productDTO)
        {
            var productEntity = _mapper.Map<Product>(productDTO);
            await _productRepository.CreateAsync(productEntity);
            //throw new NotImplementedException();
        }

        public async Task Update(ProductDto productDTO)
        {
            var productEntity = _mapper.Map<Product>(productDTO);
            await _productRepository.UpdateAsync(productEntity);
            //throw new NotImplementedException();
        }

        public async Task Remove(int id)
        {
            var productEntity = _productRepository.GetByIdAsync(id).Result;
            await _productRepository.RemoveAsync(productEntity);
            //throw new NotImplementedException();
        }
    }
}
