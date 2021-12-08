using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Queries.GetProduct;
using Application.Products.Queries.GetProductsList;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreWebApi.Models;

namespace StoreWebApi.Controllers
{
    [ApiController]
    [Route("products")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsController(IMediator mediator, IMapper mapper) => 
            (_mediator, _mapper) = (mediator, mapper);

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<ProductDto>>> GetProducts()
        {
            string categoryId = HttpContext.Request.Query["categoryId"];
            var result = await _mediator.Send(new GetProductsListQuery
            {
                CategoryId = categoryId,
            });
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

        [Authorize]
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            var createProductCommand = _mapper.Map<CreateProductCommand>(createProductDto);
            await _mediator.Send(createProductCommand);
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteProduct([FromRoute(Name = "id")] string productId)
        {
            var deleteProductCommand = new DeleteProductCommand
            {
                ProductId = productId
            };
            await _mediator.Send(deleteProductCommand);
            return Ok();
        }
    }
}
