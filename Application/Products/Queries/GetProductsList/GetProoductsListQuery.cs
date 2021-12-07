using MediatR;

namespace Application.Products.Queries.GetProductsList
{
    public class GetProductsListQuery : IRequest<ProductsListVm> 
    {
        public string CategoryId { get; set; }
    }
}