using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Screechr.Api.Entities;
using Screechr.Api.Interfaces;
using Screechr.Api.Models;

namespace Screechr.Api.Providers
{
    /// <summary>
    /// The screechrs provider concrete class.
    /// </summary>
    public class ScreechrsProvider : IScreechrsProvider
    {
        private readonly ScreechrsDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public ScreechrsProvider(ScreechrsDbContext dbContext, ILogger<ScreechrsProvider> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Users.Any())
            {
                _dbContext.Users.Add(new User() { Id = 1, FirstName = "Admin", LastName = "User", Username = "test", ProfileImageUri="", CreatedDate = new DateTimeOffset(2022, 05, 25, 0, 0, 0, 0, new TimeSpan()), ModifiedDate = new DateTimeOffset(), Password = "test" });
                _dbContext.Users.Add(new User() { Id = 2, FirstName = "Basam", LastName = "Ahmad", Username = "basam", ProfileImageUri = "https://www.linkedin.com/in/basam-ahmad/", CreatedDate = new DateTimeOffset(2022, 05, 25, 0, 0, 0, 0, new TimeSpan()), ModifiedDate = new DateTimeOffset(), Password = "test" });
                _dbContext.Users.Add(new User() { Id = 3, FirstName = "John", LastName = "Smith", Username = "john", ProfileImageUri = "", CreatedDate = new DateTimeOffset(2022, 05, 25, 0, 0, 0, 0, new TimeSpan()), ModifiedDate = new DateTimeOffset(), Password = "123" });
                _dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the users async.
        /// </summary>
        /// <returns>A Task.</returns>
        public async Task<(bool IsSuccess, IEnumerable<User> Users, string ErrorMessage)> GetUsersAsync()
        {
            try
            {
                var users = await _dbContext.Users.ToListAsync();
                if (users != null && users.Any())
                {
                    //var result = _mapper.Map<IEnumerable<User>, IEnumerable<Models.User>>(users);
                    return (true, users, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception e)
            {
                _logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Gets the user by id async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A Task.</returns>
        public async Task<(bool IsSuccess, IEnumerable<User> Users, string ErrorMessage)> GetUserByIdAsync(int id)
        {
            try
            {
                var users = await _dbContext.Users.Where(x => x.Id == id).ToListAsync();
                if (users != null && users.Any())
                {
                    //var result = _mapper.Map<IEnumerable<User>, IEnumerable<Models.UserDto>>(users);
                    return (true, users, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception e)
            {
                _logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}
