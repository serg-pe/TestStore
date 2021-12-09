using MediatR;

namespace Application.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQuery : IRequest<OrdersListVm> { }
}
