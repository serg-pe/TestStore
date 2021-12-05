using Microsoft.AspNetCore.Mvc;

namespace StoreWebApi.Controllers
{
    [ApiController]
    [Route("api/hello")]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public string Hello() => "Hello, world!";
    }
}
