using MediatR;
using ProductService.Models;
using System.Collections.Generic;

namespace ProductService.Queries
{
    public class GetAllProductsQuery : IRequest<List<Product>> { }
}

