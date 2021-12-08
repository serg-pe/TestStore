using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoreWebApi.Controllers
{
    [Route("auth")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IStoreDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IDateTimeService _dateTimeService;

        public AuthController(IStoreDbContext dbContext, IConfiguration configuration, IDateTimeService dateTimeService) =>
            (_dbContext, _configuration, _dateTimeService) = (dbContext, configuration, dateTimeService);

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> Login([FromBody()])
        {
            var user = await Authenticate(email, password);
            if (user == null)
                return Forbid();

            var token = GenerateToken(user);
            return Ok(token);
        }

        private async Task<User> Authenticate(string email, string password)
        {
            var currentUser = await _dbContext.Users.FirstOrDefaultAsync(user => user.Email.Equals(email) && user.Password.Equals(password));
            return currentUser;
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt")["Secret"]));
            var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
            };

            var token = new JwtSecurityToken(
                _configuration.GetSection("Jwt")["Issuer"],
                _configuration.GetSection("Jwt")["Audience"],
                claims,
                expires: _dateTimeService.Now.AddHours(int.Parse(_configuration.GetSection("Jwt")["Lifetime"])),
                signingCredentials: signinCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
