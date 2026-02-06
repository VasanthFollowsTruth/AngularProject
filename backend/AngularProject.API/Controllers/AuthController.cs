using Application.DTOs.Auth;
using Application.Security;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AngularProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public AuthController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequestDto request)
    {
        // TEMP: Hardcoded user (for assessment/demo)

        if (request.Email != "admin@test.com"
            || request.Password != "123456")
        {
            return Unauthorized("Invalid credentials");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, request.Email),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var token = _tokenService.GenerateToken(claims);

        return Ok(new LoginResponseDto
        {
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60)
        });
    }
}