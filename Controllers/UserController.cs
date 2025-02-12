using Microsoft.AspNetCore.Mvc;
using SafeVault.Models;
using SafeVault.Services;
using System.Linq;

namespace SafeVault.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AuthService _authService;

        public UserController(ApplicationDbContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
                return BadRequest("Email already in use.");

            user.PasswordHash = _authService.HashPassword(user.PasswordHash);
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
    var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null || !_authService.VerifyPassword(request.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials.");

            return Ok(new { Message = "Login successful", User = user.Username });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
