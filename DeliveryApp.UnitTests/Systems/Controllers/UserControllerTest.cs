using Bogus;
using DeliveryApp.API.Controllers;
using DeliveryApp.API.DataLayers;
using DeliveryApp.API.DataLayers.Entities;
using DeliveryApp.API.DataLayers.Entities.Enums;
using DeliveryApp.API.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.UnitTests.Systems.Controllers
{
    public class UserControllerTest
    {
        /*        private readonly DbContextOptions<DataContext> _options;*/

        public UserControllerTest()
        {
            /*            _options = new DbContextOptionsBuilder<DataContext>()
                        .UseInMemoryDatabase(databaseName: "DeliveryApp")
                        .Options;
            */
        }

        [Fact]
        public async Task GetUsers_ReturnsOk()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "DeliveryApp")
                .Options;

            var context = new DataContext(options);

            //test for a fake user
            var userFaker = new Faker<User>().RuleFor(u => u.UserId, f => f.IndexFaker + 1)
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Person.Email);

            context.Users.AddRange(userFaker.Generate(2));
            context.SaveChanges();

            var controller = new UsersController(context);

            // Act
            var result = await controller.GetUsers();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var users = (result as OkObjectResult).Value.Should().BeAssignableTo<IEnumerable<User>>().Subject;
            /*            users.Should().HaveCount(2);*/
            context.Dispose();
        }

        [Fact]
        public async Task GetSingleUser_ReturnsOk()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "DeliveryApp")
                .Options;

            var context = new DataContext(options);

            var userFaker = new Faker<User>().RuleFor(u => u.UserId, f => f.Random.Number(1, 1000))
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Person.Email);
            var users = userFaker.Generate();

            context.Users.AddRange(users);
            context.SaveChanges();

            var controller = new UsersController(context);

            // Act
            var result = await controller.GetSingleUser(users.UserId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var user = (result as OkObjectResult).Value.Should().BeAssignableTo<User>().Subject;
            user.UserId.Should().Be(users.UserId);
            context.Dispose(); //closing the db for gaining a little performance
        }

        [Fact]
        public async Task AddUsers_ReturnsOk()
        {
            //ARRANGE
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("DeliveryApp").Options;
            var context = new DataContext(options);
            var userDto = new UserDTO();

            var userFaker = new Faker<User>().RuleFor(u => u.UserId, f => f.Random.Number(1, 1000))
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(U => U.LastName, F => F.Person.LastName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Address, f => f.Address.FullAddress())
                .RuleFor(u => u.PostalCode, f => f.Address.ZipCode())
                .RuleFor(u => u.City, f => f.Address.City())
                .RuleFor(u => u.Region, f => f.Address.Country())
                .RuleFor(u => u.Role, f => f.PickRandom<UserRole>());

            var user = userFaker.Generate();

            context.Users.Add(user);
            context.SaveChanges();

            var controller = new UsersController(context);
            //ACT
            var result = await controller.AddUsers(userDto);

            //ASSERT
            result.Should().BeOfType<OkObjectResult>();
            var okObjResult = result as OkObjectResult;
            okObjResult.Should().NotBeNull();
            context.Dispose();
        }

        [Fact]
        public async Task UpdateUser_IfUserExistReturnOk()
        {
            //ARRANGE
            var db = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("DeliveryApp").Options;
            var context = new DataContext(db);
            var userModel = new User();
            var userId = userModel.UserId;

            var userFaker = new Faker<User>()
                                .RuleFor(u => u.UserId, f => f.Random.Number(1, 1000))
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Address, f => f.Address.FullAddress())
                .RuleFor(u => u.Region, f => f.Address.Country())
                .RuleFor(u => u.City, f => f.Address.City())
                .RuleFor(u => u.PostalCode, f => f.Address.ZipCode())
                .RuleFor(u => u.Role, f => f.PickRandom<UserRole>());
            var userToUpdate = new UserDTO
            {
                // Set properties with desired values
                FirstName = "Basem", // Replace with your desired first name
                LastName = "YourLastName", // Replace with your desired last name
                Email = "basem@example.com", // Replace with a valid email address
                Role = UserRole.User, // Choose the appropriate UserRole (e.g., Customer, Admin)
                Address = "123 Main Street", // Replace with your address
                PostalCode = "10001", // Replace with your postal code
                City = "New York", // Replace with your city
                Region = "NY", // Replace with your region (e.g., state, province)
                PhoneNumber = "(555) 555-1212", // Replace with your phone number (optional)
            };

            var user = userFaker.Generate();
            context.Users.AddRange(user);
            context.SaveChanges();

            var controller = new UsersController(context);


            //ACT
            var result = await controller.UpdateUser(user.UserId, userToUpdate);

            //ASSERT
            result.Should().BeOfType<OkObjectResult>();
            var okObjResult = result as OkObjectResult;
            okObjResult.Should().NotBeNull();
            context.Dispose();
        }

        [Fact]
        public async Task DeleteUser_ReturnsOk()
        {
            // Arrange
            var db = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("DeliveryApp")
                .Options;

            var context = new DataContext(db);


            var userModel = new User();
            var userId = userModel.UserId;

            var userFaker = new Faker<User>()
                .RuleFor(u => u.UserId, f => f.Random.Number(1, 1000))
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Address, f => f.Address.FullAddress())
                .RuleFor(u => u.Region, f => f.Address.Country())
                .RuleFor(u => u.City, f => f.Address.City())
                .RuleFor(u => u.PostalCode, f => f.Address.ZipCode())
                .RuleFor(u => u.Role, f => f.PickRandom<UserRole>());

            var generateFakeUser = userFaker.Generate();

            // Add the generated user to the database
            await context.Users.AddAsync(generateFakeUser);
            await context.SaveChangesAsync();

            // Act
            var controller = new UsersController(context);
            var result = await controller.DeleteUser(generateFakeUser.UserId);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>();

            var deletedUser = await context.Users.FindAsync(generateFakeUser.UserId);
            deletedUser.Should().BeNull(); // User should not exist after deletion
            okResult.Should().NotBeNull();
            context.Dispose();
        }

    }
}
