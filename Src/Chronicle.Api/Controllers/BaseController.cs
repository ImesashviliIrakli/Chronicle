using Chronicle.Domain.Enums;
using Chronicle.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Chronicle.Api.Controllers;

public abstract class BaseController : ControllerBase
{
    protected IActionResult CreateResponse(Result result)
    {
        if (result.Code.Equals(GlobalStatusCodes.Success))
            return Ok(result);

        switch (result.Code)
        {
            case GlobalStatusCodes.NotFound:
                return NotFound(result);
            case GlobalStatusCodes.BadRequest:
                return BadRequest(result);
            case GlobalStatusCodes.Forbidden:
                return StatusCode(403, result);
            default:
                return StatusCode(500, result);

        }
    }
    protected string GetCurrentUserId()
    {
        var userId = User.FindFirstValue("UserId");

        if (userId is null)
            throw new Exception("NotFound");

        return userId;
    }

    protected string GetCurrentUserEmail()
    {
        var email = User.FindFirstValue("Email");

        if (email is null)
            throw new Exception("NotFound");

        return email;
    }
}
