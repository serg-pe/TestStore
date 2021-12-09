using Application.Orders.Commands.CreateOrder;
using Application.Orders.Commands.DeleteOrder;
using Application.Orders.Commands.PatchOrder;
using Application.Orders.Queries.GetOrder;
using Application.Orders.Queries.GetOrdersList;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using StoreWebApi.Models;

namespace StoreWebApi.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrdersController(IMediator mediator, IMapper mapper) =>
            (_mediator, _mapper) = (mediator, mapper);

        [Authorize]
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<OrderDto>>> GetOrders()
        {
            var ordersListVm = await _mediator.Send(new GetOrdersListQuery());
            return Ok(ordersListVm.Orders);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OrderVm>> GetOrderById([FromRoute] string id)
        {
            var request = new GetOrderQuery
            {
                OrderId = Guid.Parse(id),
            };

            var orderVm = await _mediator.Send(request);
            return Ok(orderVm);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> CreateProduct([FromBody] CreateOrderDto createOrderDto)
        {            
            var createOrderCommand = _mapper.Map<CreateOrderDto, CreateOrderCommand>(createOrderDto);
            await _mediator.Send(createOrderCommand);
            return NoContent();
        }

        [Authorize]
        [HttpPatch]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> PatchProduct([FromRoute(Name = "id")] string orderId, [FromBody] JsonPatchDocument<Order> orderPatch)
        {
            var patchCommand = new PatchOrderCommand
            {
                OrderId = Guid.Parse(orderId),
                OrderPatch = orderPatch,
            };
            await _mediator.Send(patchCommand);
            return NoContent();
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteProduct([FromRoute(Name = "id")] string orderId)
        {
            var deleteProductCommand = new DeleteOrderCommand
            {
                OrderId = Guid.Parse(orderId),
            };
            await _mediator.Send(deleteProductCommand);
            return NoContent();
        }
    }
}
