using DeliveryApp.API.Controllers;
using DeliveryApp.API.DataLayers;
using DeliveryApp.API.DataLayers.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.UnitTests.Systems.Controllers
{
    public class UserControllerTest
    {
        [Fact]
        public async Task GetUsers_ReturnsOk()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "DeliveryApp")
                .Options;

            // Instantiate DataContext
            var context = new DataContext(options);
            context.Users.AddRange(new[]
            {
                new User { UserId = 1, FirstName = "John", LastName = "Doe" },
                new User { UserId = 2, FirstName = "Jane", LastName = "Smith" }
            });
            context.SaveChanges();

            var controller = new UserController(context);

            // Act
            var result = await controller.GetUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var users = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
            Assert.Equal(2, users.Count()); // Assuming two users were added in Arrange step

            // Dispose of DataContext
            context.Dispose();
        }

        [Fact]
        public async Task GetSingleUser_ReturnsOk()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "DeliveryApp")
                .Options;

            // Instantiate DataContext
            var context = new DataContext(options);
            context.Users.Add(new User { UserId = 3, FirstName = "Andrei", LastName = "Alionte" });
            context.SaveChanges();

            var controller = new UserController(context);

            // Act
            var result = await controller.GetSingleUser(3);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var user = Assert.IsAssignableFrom<User>(okResult.Value);
            Assert.Equal(3, user.UserId);

            // Dispose of DataContext
            context.Dispose();
        }
    }
}
