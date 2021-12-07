using Application.Products.Queries.GetProduct;
using Application.Products.Queries.GetProductsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace StoreWebApi.Controllers
{
    [ApiController]
    [Route("products")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<ProductDto>>> GetAllProducts()
        {
            var result = await _mediator.Send(new GetProductsListQuery());
            return Ok(result.Products);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductVm>> GetProductById(string id)
        {
            var request = new GetProductQuery
            {
                ProductId = id,
            };

            var product = await _mediator.Send(request);
            return Ok(product);
        }
    }
}
