using ProductService.Data;
using ProductService.Models;
using ProductService.Commands;
using Microsoft.EntityFrameworkCore;

namespace ProductService.Services
{
    public class ProductServiceImpl
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductServiceImpl(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<Product>> GetAllAsync() => await _context.Products.ToListAsync();

        public async Task<Product> GetByIdAsync(Guid id) => await _context.Products.FindAsync(id);

            public async Task AddAsync(CreateProductCommand command)
                    {
                        var product = new Product
                        {
                            Id = Guid.NewGuid(),
                            ProductName = command.ProductName,
                            ProductCategoryName = command.ProductCategoryName,
                            Manufacturer = command.Manufacturer,
                            Quantity = command.Quantity,
                            Price = command.Price,
                            ProductImage = command.ProductImage // Use image URL
                        };

                        _context.Products.Add(product);
                        await _context.SaveChangesAsync();
                    }

                    public async Task UpdateAsync(Product product)
                    {
                        _context.Products.Update(product);
                        await _context.SaveChangesAsync();
                    }

                    public async Task DeleteAsync(Guid id)
                    {
                        var product = await _context.Products.FindAsync(id);
                        if (product != null)
                        {
                            _context.Products.Remove(product);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
}
