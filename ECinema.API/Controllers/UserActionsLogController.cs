using ECinema.Data.MediatR.UserActionLog;
using ECinema.Data.MediatR.UserActionsLog;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UserActionsLogController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserActionsLogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllUserActionLogs()
    {
        var result = await _mediator.Send(new SelectAllUserActionLogsRequest());
        if (result != null && result.Count > 0)
        {
            return Ok(result);
        }
        return NotFound("No user action logs found.");
    }


    [HttpGet("by-action-type/{actionType}")]
    public async Task<IActionResult> GetUserActionLogsByActionType(string actionType)
    {
        var result = await _mediator.Send(new SelectUserActionLogsByActionTypeRequest() { ActionType = actionType });
        if (result != null && result.Count > 0)
        {
            return Ok(result);
        }
        return NotFound($"No user action logs found for action type '{actionType}'.");
    }
}
