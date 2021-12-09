using MediatR;

namespace Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest<Unit>
    {
        public Guid OrderId { get; set; }
    }
}
