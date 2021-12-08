using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StoreWebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoreWebApi.Controllers
{
    [ApiController]
    [Route("auth")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IDateTimeService _dateTimeService;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration, IDateTimeService dateTimeService) =>
            (_userManager, _configuration, _dateTimeService) = (userManager, configuration, dateTimeService);

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Login([FromBody] LoginVm loginVm)
        {
            var user = await _userManager.FindByEmailAsync(loginVm.Email);
            if (user == null)
                return Unauthorized();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
            };

            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt")["Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("Jwt")["Issuer"],
                audience: _configuration.GetSection("Jwt")["Audience"],
                expires: _dateTimeService.Now.AddHours(int.Parse(_configuration.GetSection("Jwt")["Lifetime"])),
                signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                );

            return Ok(new
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                User = new
                {
                    Email = user.Email,
                    Id = user.Id,
                }
            });
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register([FromBody] LoginVm userCredentials)
        {
            var user = await _userManager.FindByEmailAsync(userCredentials.Email);
            if (user != null)
                return BadRequest();

            user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = userCredentials.Email,
                UserName = userCredentials.Email,
            };

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
                return Ok();
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
