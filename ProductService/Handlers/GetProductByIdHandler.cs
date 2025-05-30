using MediatR;
using ProductService.Queries;
using ProductService.Models;
using ProductService.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ProductService.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly ApplicationDbContext _context;

        public GetProductByIdHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products.FindAsync(request.Id);
        }
    }
}
