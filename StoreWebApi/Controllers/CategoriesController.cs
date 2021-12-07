using Application.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace StoreWebApi.Controllers
{
    [Route("category")]
    [ApiController]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CategoriesListVm>> GetAllCategories()
        {
            var vm = await _mediator.Send(new GetCategoriesListQuery());
            return Ok(vm.Categories);
        }
    }
}
