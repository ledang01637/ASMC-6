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
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.EntityFrameworkCore;

namespace ASMC6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthJWTController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDBContext _appDBContext;
        private readonly ILogger<AuthJWTController> _logger;
        private User userLogin = new User();

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

        [HttpGet("signin-google")]
        public IActionResult SignInWithGoogle()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
            {
                return Ok(new LoginRespone
                {
                    SuccsessFull = false,
                    Error = "Đăng nhập thất bại"
                });
            }
            var emailClaim = result.Principal.FindFirst(ClaimTypes.Email);

            userLogin = _appDBContext.User
            .FirstOrDefault(x => x.Email == emailClaim.Value);

            if (userLogin is null)
            {
                var newUser = new User
                {
                    Name = result.Principal.FindFirst(ClaimTypes.Name)?.Value,
                    Email = emailClaim.Value,
                    Password = new Random().Next(100000, 999999).ToString(),
                    RoleId = 3,
                    Address = "",
                    Phone = "",
                    IsDelete = false

                };
                _appDBContext.User.Add(newUser);
                _appDBContext.SaveChanges();

                userLogin = newUser;
            }

            var token = GenerateJwtToken(userLogin);
          
            return LocalRedirectPreserveMethod($"/ggfb-response?token={token}");

        }

        [HttpGet("signin-facebook")]
        public IActionResult SignInWithFacebook()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("FacebookResponse")
            };
            return Challenge(properties, FacebookDefaults.AuthenticationScheme);
        }

        [HttpGet("facebook-response")]
        public async Task<IActionResult> FacebookResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
            {
                return Ok(new LoginRespone
                {
                    SuccsessFull = false,
                    Error = "Đăng nhập thất bại"
                });
            }
            var emailClaim = result.Principal.FindFirst(ClaimTypes.Email);

            userLogin = _appDBContext.User
                .FirstOrDefault(x => x.Email == emailClaim.Value);

            if (userLogin is null)
            {
                var newUser = new User
                {
                    Name = result.Principal.FindFirst(ClaimTypes.Name)?.Value,
                    Email = emailClaim.Value,
                    Password = new Random().Next(100000, 999999).ToString(),
                    RoleId = 3,
                    Address = "",
                    Phone = "",
                    IsDelete = false

                };
                _appDBContext.User.Add(newUser);
                _appDBContext.SaveChanges();
                userLogin = newUser;
            }

            var token = GenerateJwtToken(userLogin);
            return LocalRedirectPreserveMethod($"/ggfb-response?token={token}");
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
                new Claim("Name", user.Name),
                new Claim("RoleId", user.RoleId.ToString()),
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
