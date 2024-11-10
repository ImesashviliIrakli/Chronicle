using Chronicle.Application.Identity.Commands.Login;
using Chronicle.Application.Identity.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Chronicle.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class IdentityController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Login(LoginCommand loginCommand)
    {
        var data = await _mediator.Send(loginCommand);

        return CreateResponse(data);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterCommand registerCommand)
    {
        var data = await _mediator.Send(registerCommand);

        return CreateResponse(data);
    }

    [HttpGet("login-google")]
    public IActionResult GetGoogleLoginUrl()
    {
        var redirectUrl = Url.Action("GoogleCallback", "Identity");
        var properties = new AuthenticationProperties
        {
            RedirectUri = redirectUrl
        };

        var googleUrl = Url.Action("ExternalLogin", "Identity", new { provider = "Google", returnUrl = redirectUrl });
        return Ok(new { Url = googleUrl });
    }


    // Callback endpoint for handling the Google OAuth response.
    [HttpGet("google-callback")]
    public async Task<IActionResult> GoogleCallback()
    {
        // Authenticate the user using the scheme set up with Google.
        var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

        // If the authentication failed, return an error.
        if (!authenticateResult.Succeeded || authenticateResult.Principal == null)
            return Unauthorized("Google authentication failed.");

        // Extract user information from Google claims.
        var email = authenticateResult.Principal.FindFirst(ClaimTypes.Email)?.Value;
        var name = authenticateResult.Principal.FindFirst(ClaimTypes.Name)?.Value;

        // Handle user information here, e.g., creating or logging in a user in your system.
        // Example: Check if user exists; if not, create a new user; if exists, log them in.

        // For simplicity, let's return the user's email and name as a sample response.
        return Ok(new
        {
            Message = "Google authentication succeeded.",
            Email = email,
            Name = name
        });
    }

    [HttpGet("external-login")]
    public IActionResult ExternalLogin(string provider, string returnUrl)
    {
        var properties = new AuthenticationProperties { RedirectUri = returnUrl };
        return Challenge(properties, provider); // Redirects to the external authentication provider (Google)
    }
}
