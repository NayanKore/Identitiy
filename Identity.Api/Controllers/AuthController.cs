using Identity.Application.Features.Auth.Commands;
using Identity.Application.Features.Auth.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto request)
        {
            var result = await _mediator.Send(new LoginCommand(request.Email, request.Password));
            return Ok(result);
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin")]   // Only admins can register new users
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            await _mediator.Send(new RegisterCommand(
                request.EmployeeId,
                request.FullName,
                request.Email,
                request.Password,
                request.Role));
            return Ok(new { message = "User registered successfully." });
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            return Ok(new { UserId = userId, User.Identity.Name });
        }
    }
}
