using MediatR;
using ProductService.Models;

namespace ProductService.Commands
{
    public class UpdateProductCommand : IRequest<Product>
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCategoryName { get; set; }
        public string Manufacturer { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductImage { get; set; }
    }
}
