using Microsoft.AspNetCore.Mvc;
using MediatR;
using ECinema.Data.MediatR.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using ECinema.Data.Entities;
using Org.BouncyCastle.Asn1.Ocsp;
using ECinema.API.Auth;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly JwtSettings _jwtSettings;

    public UsersController(IMediator mediator, IOptions<JwtSettings> jwtSettings)
    {
        _mediator = mediator;
        _jwtSettings = jwtSettings.Value;
    }

    [HttpPost("check-credentials")]
    public async Task<IActionResult> CheckUserCredentials([FromBody] CheckUserCredentialsRequest request)
    {
        var result = await _mediator.Send(request);

        if (result == null)
        {
            return NotFound("No such email-password combination");
        }
        var (userId, roleId) = result.Value;

        // Генерация JWT
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Role, roleId.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
        issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { Token = tokenString });
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUserById(int userId)
    {
        if (userId == 0)
            return Forbid();
        var result = await _mediator.Send(new DeleteUserByIdRequest { UserId = userId });
        if (result)
            return NoContent();
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> InsertUser([FromBody] InsertUserRequest request)
    {

        try
        {
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && request.RoleId != 1)
            {
                return Forbid();
            }

            var result = await _mediator.Send(request);
            if (result)
                return Ok(result);
            return BadRequest();
        }catch(Exception ex)
        {
            return BadRequest();
        }
    }

    [Authorize(Roles ="Admin")]
    [HttpGet]
    public async Task<IActionResult> SelectAllUsers()
    {
        var result = await _mediator.Send(new SelectAllUsersQueryRequest());
        return Ok(result);
    }


    [Authorize(Roles = "Admin,User")]
    [HttpGet("{userId}")]
    public async Task<IActionResult> SelectUserById(int userId)
    {
        var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var isAdmin = User.IsInRole("Admin");

        if (!isAdmin && userIdFromToken != userId.ToString())
        {
            return Forbid();
        }

        var result = await _mediator.Send(new SelectUserByIdRequest { UserId = userId });
        if (result != null)
            return Ok(result);
        return NotFound();
    }

    [Authorize(Roles = "User,Admin")]
    [HttpPut("email")]
    public async Task<IActionResult> UpdateUserEmail([FromBody] UpdateUserEmailRequest request)
    {
        var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var isAdmin = User.IsInRole("Admin");

        if (!isAdmin && userIdFromToken != request.UserId.ToString())
        {
            return Forbid();  
        }


        var result = await _mediator.Send(request);
        if (result)
            return NoContent();
        return NotFound();
    }

    [Authorize(Roles = "User,Admin")]
    [HttpPut("password")]
    public async Task<IActionResult> UpdateUserPassword([FromBody] UpdateUserPasswordRequest request)
    {
        var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var isAdmin = User.IsInRole("Admin");

        if (!isAdmin && userIdFromToken != request.UserId.ToString())
        {
            return Forbid();
        }

        var result = await _mediator.Send(request);
        if (result)
            return NoContent();
        return NotFound();
    }
}
