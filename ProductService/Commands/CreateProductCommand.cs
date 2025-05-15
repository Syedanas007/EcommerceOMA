using MediatR;
using Microsoft.AspNetCore.Http;
using ProductService.Models;

namespace ProductService.Commands
{
    public class CreateProductCommand : IRequest<Product>
    {
        public string ProductName { get; set; }
        public string ProductCategoryName { get; set; }
        public string Manufacturer { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public IFormFile ProductImage { get; set; } 
    }
}
