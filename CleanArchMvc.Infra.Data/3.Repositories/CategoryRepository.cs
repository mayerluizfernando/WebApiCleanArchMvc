using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data._1.Context;
using Microsoft.EntityFrameworkCore;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Infra.Data._3.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationDbContext _categoryDbContext; 
        public CategoryRepository(ApplicationDbContext dbContext) {
            _categoryDbContext = dbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            _categoryDbContext.Add(category);
            await _categoryDbContext.SaveChangesAsync();
            return category;

            //throw new NotImplementedException();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _categoryDbContext.Categories.FindAsync(id);
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _categoryDbContext.Categories.ToListAsync<Category>();
            //throw new NotImplementedException();
        }

        public async Task<Category> RemoveAsync(Category category)
        {
            _categoryDbContext.Remove(category);
            await _categoryDbContext.SaveChangesAsync();
            return category;
            //throw new NotImplementedException();
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            _categoryDbContext.Update(category);
            await _categoryDbContext.SaveChangesAsync();
            return category;
            //throw new NotImplementedException();
        }
    }
}
