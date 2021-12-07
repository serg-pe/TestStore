using Application.Products.Queries;
using MediatR;

namespace StoreTests.Products.Queries
{
    public class GetProductsListQuery : IRequest<ProductsListVm> { }
}