using Microsoft.AspNetCore.Mvc;
using MediatR;
using ECinema.Data.MediatR.WatchHistory;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Org.BouncyCastle.Asn1.Ocsp;

[ApiController]
[Route("api/[controller]")]
public class WatchHistoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public WatchHistoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Admin,User")]
    [HttpPost]
    public async Task<IActionResult> InsertWatchHistory([FromBody] InsertWatchHistoryRequest request)
    {
        var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var isAdmin = User.IsInRole("Admin");

        if (!isAdmin && userIdFromToken != request.UserId.ToString())
        {
            return Forbid();
        }

        var watchId = await _mediator.Send(request);
        return CreatedAtAction(nameof(SelectWatchHistoryByUser), new { userId = request.UserId }, new { WatchId = watchId });
    }

    [Authorize(Roles = "Admin,User")] 
    [HttpDelete]
    public async Task<IActionResult> DeleteWatchHistory([FromBody] DeleteWatchHistoryRequest request)
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

    [Authorize(Roles = "Admin,User")]
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> SelectWatchHistoryByUser(int userId)
    {
        var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var isAdmin = User.IsInRole("Admin");

        if (!isAdmin && userIdFromToken != userId.ToString())
        {
            return Forbid();
        }


        var watchHistory = await _mediator.Send(new SelectWatchHistoryByUserRequest { UserId = userId });
        if (watchHistory != null && watchHistory.Any())
            return Ok(watchHistory);
        return NotFound();
    }
}
