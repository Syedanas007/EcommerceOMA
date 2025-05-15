using CategoryService.Data;
using CategoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace CategoryService.Services
{
    public class CategoryServiceImpl 
    {
        private readonly ApplicationDbContext _context;

        public CategoryServiceImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync() => await _context.Categories.ToListAsync();

        public async Task<Category> GetByIdAsync(Guid id) => await _context.Categories.FindAsync(id);

        public async Task AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat != null)
            {
                _context.Categories.Remove(cat);
                await _context.SaveChangesAsync();
            }
        }
    }
}
