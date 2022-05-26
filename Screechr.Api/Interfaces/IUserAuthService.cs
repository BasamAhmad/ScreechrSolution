using Screechr.Api.Models;

namespace Screechr.Api.Interfaces
{
    public interface IUserAuthService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        //User GetById(int id);
    }
}
