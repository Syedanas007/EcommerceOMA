using MediatR;
using ProductService.Commands;
using ProductService.Models;
using ProductService.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ProductService.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly ApplicationDbContext _context;

        public UpdateProductHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(request.Id);
            if (product != null)
            {
                product.ProductName = request.ProductName;
                product.ProductCategoryName = request.ProductCategoryName;
                product.Manufacturer = request.Manufacturer;
                product.Quantity = request.Quantity;
                product.Price = request.Price;
                product.ProductImage = request.ProductImage;

                await _context.SaveChangesAsync();
            }
            return product;
        }
    }
}
