using MediatR;

namespace Application.Orders.Queries.GetOrder
{
    public class GetOrderQuery : IRequest<OrderVm>
    {
        public Guid OrderId { get; set; }
    }
}
