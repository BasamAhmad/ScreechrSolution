namespace Screechr.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Screechr.Api.Helpers;
using Screechr.Api.Interfaces;
using Screechr.Api.Models;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserAuthService _userAuthService;
    private readonly IScreechrsProvider _screechrsProvider;

    public UsersController(IScreechrsProvider screechrsProvider, IUserAuthService userAuthService) 
    {
        _userAuthService = userAuthService;
        _screechrsProvider = screechrsProvider;
        _userAuthService = userAuthService;
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userAuthService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }


    /// <summary>
    /// Gets the users.
    /// </summary>
    /// <returns>A Task.</returns>
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var (IsSuccess, Users, ErrorMessage) = await _screechrsProvider.GetUsersAsync();
        if (IsSuccess)
        {
            return Ok(Users);
        }
        return NotFound();
    }

    /// <summary>
    /// Gets the user by id.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>An IActionResult.</returns>
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var (IsSuccess, Users, ErrorMessage) = await _screechrsProvider.GetUserByIdAsync(id);
        if (IsSuccess)
        {
            return Ok(Users);
        }
        return NotFound();
    }
}
