using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data._1.Context;
using Microsoft.EntityFrameworkCore;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Infra.Data._3.Repositories
{

    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext _productDbContext;
        public ProductRepository(ApplicationDbContext dbContext)
        {
            _productDbContext = dbContext;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _productDbContext.Products.Add(product);
            await _productDbContext.SaveChangesAsync(); 
            return product;
            //throw new NotImplementedException();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productDbContext.Products.FindAsync(id);
            //throw new NotImplementedException();
        }

        public async Task<Product> GetProductCategoryAsync(int id)
        {
            
            return await _productDbContext.Products.Include(c => c.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productDbContext.Products.ToListAsync();
            //throw new NotImplementedException();
        }

        public async Task<Product> RemoveAsync(Product product)
        {
            _productDbContext.Remove(product);
            await _productDbContext.SaveChangesAsync();
            return product;
            //throw new NotImplementedException();
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _productDbContext.Update(product);
            await _productDbContext.SaveChangesAsync();
            return product;
            //throw new NotImplementedException();
        }
    }
}
