using Application.DTOs.Auth;
using Application.Services.Auth;
using Application.Services.User;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            var existingUser = await _userService.GetUserByUsernameAsync(dto.Username);
            if (existingUser != null)
            {
                return Conflict("Username already exists");
            }

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "User"
            };

            await _userService.AddUserAsync(user);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password))
            {
                return BadRequest("Invalid login request");
            }

            var token = await _authService.AuthenticateAsync(dto.Username, dto.Password);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok(new AuthResponseDto
            {
                Token = token,
                Username = dto.Username,
                Role = "User"
            });
        }
    }
}
