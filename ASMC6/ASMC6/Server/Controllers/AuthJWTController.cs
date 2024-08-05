using ASMC6.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using ASMC6.Server.Data;

namespace ASMC6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthJWTController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDBContext _appDBContext;
        private readonly ILogger<AuthJWTController> _logger;

        public AuthJWTController(IConfiguration configuration, AppDBContext appDBContext, ILogger<AuthJWTController> logger)
        {
            _configuration = configuration;
            _appDBContext = appDBContext;
            _logger = logger;
        }

        [HttpPost("AuthUser")]
        public IActionResult Login([FromBody] LoginRequest loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var successLogin = _appDBContext.User
                .FirstOrDefault(x => x.Email == loginModel.Email && x.Password == loginModel.Password);

            if (successLogin != null)
            {
                var token = GenerateJwtToken(successLogin);
                return Ok(new LoginRespone
                {
                    SuccsessFull = true,
                    Token = token
                });
            }
            else
            {
                return Ok(new LoginRespone
                {
                    SuccsessFull = false,
                    Error = "Tài khoản hoặc mật khẩu không chính xác."
                });
            }
        }

        private string GenerateJwtToken(User user)
        {
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Id", user.UserId.ToString()),
                new Claim("Email", user.Email),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: signIn
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
