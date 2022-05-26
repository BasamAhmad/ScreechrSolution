namespace Screechr.Api.Helpers;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Screechr.Api.Interfaces;
using Screechr.Api.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    private readonly ScreechrsDbContext _dbContext;
    private readonly ILogger _logger;
    //private readonly IMapper _mapper;
    //private readonly IScreechrsProvider _screechrsProvider;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings, ILogger<JwtMiddleware> logger)
    {
        _next = next;
        _appSettings = appSettings.Value;
        _logger = logger;
    }

    //public async Task Invoke(HttpContext context, IUserAuthService userAuthService)
    public async Task Invoke(HttpContext context, ScreechrsDbContext dbContext)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            //attachUserToContext(context, userAuthService, token);
            attachUserToContext(context, dbContext, token);
        }
        await _next(context);
    }

    private async void attachUserToContext(HttpContext context, ScreechrsDbContext dbContext, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            // attach user to context on successful jwt validation
            //context.Items["User"] = userAuthService.GetById(userId);

            //var result = _mapper.Map<IEnumerable<User>, IEnumerable<Models.User>>(users);
            //context.Items["User"] = _screechrsProvider.GetUserById(userId);
            if (dbContext != null)
            {
                var users = await dbContext.Users.Where(x => x.Id == userId).ToListAsync();
                if (users != null && users.Any())
                {
                    context.Items["User"] = users.FirstOrDefault();
                }
            }
        }
        catch
        {
            // do nothing if jwt validation fails
            // user is not attached to context so request won't have access to secure routes
        }
    }
}