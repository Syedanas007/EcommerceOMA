using MediatR;
using ProductService.Commands;
using ProductService.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ProductService.Handlers
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly ApplicationDbContext _context;

        public DeleteProductHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(request.Id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }

            return Unit.Value; // Return Unit to satisfy MediatR
        }
    }
}
