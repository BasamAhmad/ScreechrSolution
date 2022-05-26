using Moq;
using Screechr.Api.Controllers;
using Screechr.Api.Interfaces;
using Screechr.Api.Models;

namespace Screechr.Api.UnitTest
{
    public class AuthenticateControllerUnitTest
    {
        [Fact]
        public void Test1()
        {
            //TODO: Add test cases
        }
        /// <summary>
        /// Tests the authenticate user.
        /// </summary>
        [Fact]
        public void TestAuthenticateUser()
        {
            // Arrange
            var authenticateRequest = new AuthenticateRequest();
            authenticateRequest.Username = "test";
            authenticateRequest.Password = "123";

            var mockUserAuthService = new Mock<IUserAuthService>();
            mockUserAuthService.Setup(x => x.Authenticate(authenticateRequest));

            var mockScreechrsProvider = new Mock<IScreechrsProvider>();
            mockScreechrsProvider.Setup(x => x.GetUsersAsync());

            var authenticateController = new AuthenticateController(mockUserAuthService.Object, mockScreechrsProvider.Object);

            // Act
            var result = authenticateController.Authenticate(authenticateRequest);

            // Assert
            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value.ToString(), "{ message = Username or password is incorrect }");
        }
    }
}