namespace Screechr.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Screechr.Api.Interfaces;
using Screechr.Api.Models;

/// <summary>
/// The authenticate controller.
/// </summary>

[ApiController]
[Route("[controller]")]
public class AuthenticateController : ControllerBase
{
    private readonly IUserAuthService _userAuthService;
    private readonly IScreechrsProvider _screechrsProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticateController"/> class.
    /// </summary>
    /// <param name="screechrsProvider">The screechrs provider.</param>
    /// <param name="userAuthService">The user auth service.</param>
    public AuthenticateController(IUserAuthService userAuthService, IScreechrsProvider screechrsProvider) 
    {
        _userAuthService = userAuthService;
        _screechrsProvider = screechrsProvider;
    }

    /// <summary>
    /// Authenticates the user.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>An IActionResult.</returns>
    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userAuthService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }
}
