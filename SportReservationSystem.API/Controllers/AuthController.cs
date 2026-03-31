using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportReservationSystem.API.Services.Interfaces;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        
        // GET: api/auth/user/{id}
        [HttpGet("user/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            
            // Un client ne peut voir que son propre profil
            if (currentUserId != id && !User.IsInRole("Gestionnaire"))
            {
                return Forbid();
            }
            
            var result = await _authService.GetUserByIdAsync(id);
            
            if (!result.Success)
                return NotFound(new { message = result.Message });
            
            return Ok(result);
        }

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            
            if (!result.Success)
                return BadRequest(new { message = result.Message });
            
            return Ok(result);
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);
            
            if (!result.Success)
                return Unauthorized(new { message = result.Message });
            
            return Ok(result);
        }
    }
}