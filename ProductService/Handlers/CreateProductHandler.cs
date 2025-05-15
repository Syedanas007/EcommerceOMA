using MediatR;
using ProductService.Commands;
using ProductService.Models;
using ProductService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ProductService.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CreateProductHandler(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            string imageFileName = null;

            if (request.ProductImage != null && request.ProductImage.Length > 0)
            {
                // Create unique file name to avoid conflicts
                imageFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.ProductImage.FileName);

                // Path to wwwroot/images folder (create if not exists)
                var imagePath = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(imagePath))
                    Directory.CreateDirectory(imagePath);

                var filePath = Path.Combine(imagePath, imageFileName);

                // Save the file to disk
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.ProductImage.CopyToAsync(stream, cancellationToken);
                }
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = request.ProductName,
                ProductCategoryName = request.ProductCategoryName,
                Manufacturer = request.Manufacturer,
                Quantity = request.Quantity,
                Price = request.Price,
                ProductImage = imageFileName // Store filename in DB
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            return product;
        }
    }
}
