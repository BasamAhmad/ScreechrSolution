using Screechr.Api.Models;
using Xunit;
using Xunit.Sdk;

namespace Screechr.Api.Interfaces
{
    public interface IScreechrsProvider
    {
        /// <summary>
        /// Gets the users async.
        /// </summary>
        /// <returns>A Task.</returns>
        Task<(bool IsSuccess, IEnumerable<User> Users, string ErrorMessage)> GetUsersAsync();
        /// <summary>
        /// Gets the user by id async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        Task<(bool IsSuccess, IEnumerable<User> Users, string ErrorMessage)> GetUserByIdAsync(int id);
    }
}
