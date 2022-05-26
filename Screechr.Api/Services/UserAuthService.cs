namespace Screechr.Api.Services;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Screechr.Api.Models;
using Screechr.Api.Helpers;
using Screechr.Api.Interfaces;
using Screechr.Api.Entities;
using AutoMapper;

/// <summary>
/// The user auth service.
/// </summary>

public class UserAuthService : IUserAuthService
{
    private readonly AppSettings _appSettings;
    private readonly ScreechrsDbContext _dbContext;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserAuthService"/> class.
    /// </summary>
    /// <param name="appSettings">The app settings.</param>
    public UserAuthService(IOptions<AppSettings> appSettings, ScreechrsDbContext dbContext, ILogger<UserAuthService> logger, IMapper mapper)
    {
        _appSettings = appSettings.Value;
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    /// Authenticates the.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>An AuthenticateResponse.</returns>
    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _dbContext.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

        // return null if user not found
        if (user == null)
        {
            _logger?.LogError("User not found");
            return null;
        }

        //var user = _mapper.Map<IEnumerable<Entities.User>, IEnumerable<Models.User>>(selectedUser);
        // authentication successful so generate jwt token
        var token = generateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }

    /// <summary>
    /// generates the jwt token.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>A string.</returns>
    private string generateJwtToken(User user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}