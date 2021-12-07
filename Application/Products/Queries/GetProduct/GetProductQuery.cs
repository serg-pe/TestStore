using MediatR;

namespace Application.Products.Queries.GetProduct
{
    public class GetProductQuery : IRequest<ProductVm>
    {
        public string ProductId { get; set; }
    }
}
