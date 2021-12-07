﻿using Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreTests.Products.Queries;

namespace StoreWebApi.Controllers
{
    [ApiController]
    [Route("products")]
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
    }
}