using AuthAPI.Data;
using AuthAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        //  REGISTRO
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("Usuario registrado");
        }

        //  LOGIN 
        [HttpPost("login")]
        public IActionResult Login(User loginUser)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == loginUser.Email);

            if (user == null)
                return Unauthorized("Usuario no existe");

            bool isValid = BCrypt.Net.BCrypt.Verify(loginUser.PasswordHash, user.PasswordHash);

            if (!isValid)
                return Unauthorized("Contraseña incorrecta");

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                HttpContext.RequestServices.GetRequiredService<IConfiguration>()["Jwt:Key"]
            ));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "AuthAPI",
                audience: "AuthAPIUsers",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = jwt });
        }
        //  ENDPOINT PROTEGIDO 
        [HttpGet("profile")]
        [Authorize]
        public IActionResult GetProfile()
        {
            var userEmail = User.Identity.Name;
            return Ok($"Perfil del usuario: {userEmail}");
        }
    }
}

