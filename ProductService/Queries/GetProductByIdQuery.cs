using MediatR;
using ProductService.Models;

namespace ProductService.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public Guid Id { get; set; }
    }
}
