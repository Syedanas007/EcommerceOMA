using MediatR;
using ProductService.Commands;
using ProductService.Models;
using ProductService.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProductService.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly ApplicationDbContext _context;

        public CreateProductHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Since image is passed as URL, no file saving needed
            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = request.ProductName,
                ProductCategoryName = request.ProductCategoryName,
                Manufacturer = request.Manufacturer,
                Quantity = request.Quantity,
                Price = request.Price,
                ProductImage = request.ProductImage // Store URL directly
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            return product;
        }
    }
}
