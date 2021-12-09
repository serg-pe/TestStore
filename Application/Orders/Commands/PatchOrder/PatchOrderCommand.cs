using Domain;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Orders.Commands.PatchOrder
{
    public class PatchOrderCommand : IRequest<Unit>
    {
        public Guid OrderId { get; set; }
        public JsonPatchDocument<Order> OrderPatch { get; set; } 
    }
}
