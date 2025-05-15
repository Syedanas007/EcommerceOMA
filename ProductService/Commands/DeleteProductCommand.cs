using MediatR;
using System;

namespace ProductService.Commands
{
    public class DeleteProductCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
