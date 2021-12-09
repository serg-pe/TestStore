using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.PatchProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Queries.GetProduct;
using Application.Products.Queries.GetProductsList;
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
    [Route("api/products")]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            var createProductCommand = _mapper.Map<CreateProductCommand>(createProductDto);
            await _mediator.Send(createProductCommand);
            return NoContent();
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateProduct([FromRoute(Name = "id")] string productId, [FromBody] UpdateProductDto updateProductDto)
        {
            var updateProductCommand = _mapper.Map<UpdateProductDto, UpdateProductCommand>(updateProductDto);
            updateProductCommand.ProductId = Guid.Parse(productId);
            await _mediator.Send(updateProductCommand);
            return NoContent();
        }

        [Authorize]
        [HttpPatch]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> PatchProduct([FromRoute(Name = "id")] string productId, [FromBody] JsonPatchDocument<Product> productPatch)
        {
            var patchCommand = new PatchProductCommand
            {
                ProductId = productId,
                ProductPatch = productPatch,
            };
            await _mediator.Send(patchCommand);
            return NoContent();
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteProduct([FromRoute(Name = "id")] string productId)
        {
            var deleteProductCommand = new DeleteProductCommand
            {
                ProductId = productId
            };
            await _mediator.Send(deleteProductCommand);
            return NoContent();
        }
    }
}
