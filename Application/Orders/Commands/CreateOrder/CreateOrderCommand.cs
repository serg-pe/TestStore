using Domain;
using MediatR;

namespace Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Unit>
    {
        public IEnumerable<Product> Products { get; set; }
        public Client Client { get; set; }
        public string Status { get; set; }
    }
}
