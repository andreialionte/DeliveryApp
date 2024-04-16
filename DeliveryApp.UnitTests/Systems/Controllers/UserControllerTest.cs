using Bogus;
using DeliveryApp.API.Controllers;
using DeliveryApp.API.DataLayers;
using DeliveryApp.API.DataLayers.Entities;
using DeliveryApp.API.DataLayers.Entities.Enums;
using DeliveryApp.API.DTOs;
using DeliveryApp.API.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DeliveryApp.UnitTests.Systems.Controllers
{
    public class UserControllerTest
    {
        /*private readonly DbContextOptions<DataContext> _options;*/
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
            var mockRepository = new Mock<IUserRepository>();
            var mockDataContext = new Mock<DataContext>(new DbContextOptions<DataContext>());

            //test for a fake user
            var userFaker = new Faker<User>().RuleFor(u => u.UserId, f => f.IndexFaker + 1)
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Person.Email);

            var fakeUsers = userFaker.Generate(2);
            mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(fakeUsers);

            var controller = new UsersController(mockDataContext.Object, mockRepository.Object);

            // Act
            var result = await controller.GetUsers();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var users = (result as OkObjectResult).Value.Should().BeAssignableTo<IEnumerable<User>>().Subject;
            users.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetSingleUser_ReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var mockDataContext = new Mock<DataContext>(new DbContextOptions<DataContext>());

            var userFaker = new Faker<User>().RuleFor(u => u.UserId, f => f.Random.Number(1, 1000))
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Person.Email);
            var users = userFaker.Generate();
            mockRepository.Setup(r => r.GetById(users.UserId)).ReturnsAsync(users);

            var controller = new UsersController(mockDataContext.Object, mockRepository.Object);

            // Act
            var result = await controller.GetSingleUser(users.UserId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var user = (result as OkObjectResult).Value.Should().BeAssignableTo<User>().Subject;
            user.UserId.Should().Be(users.UserId);
        }

        [Fact]
        public async Task AddUsers_ReturnsOk()
        {
            //ARRANGE
            /*            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("DeliveryApp").Options;
                        var context = new DataContext(options);*/
            var moqRepository = new Mock<IUserRepository>();
            var moqDataContext = new Mock<DataContext>(new DbContextOptions<DataContext>());
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
            moqRepository.Setup(r => r.Add(user)).ReturnsAsync(user);

            /*            context.Users.Add(user);
                        context.SaveChanges();*/

            var controller = new UsersController(moqDataContext.Object, moqRepository.Object);
            //ACT
            var result = await controller.AddUsers(userDto);

            //ASSERT
            result.Should().BeOfType<OkObjectResult>();
            var okObjResult = result as OkObjectResult;
            okObjResult.Should().NotBeNull();
            /*            context.Dispose();*/
        }


        [Fact]
        public async Task UpdateUser_IfUserExistReturnOk()
        {
            // Arrange
            var moqRepository = new Mock<IUserRepository>();
            var moqDataContext = new Mock<DataContext>(new DbContextOptions<DataContext>());

            var userFaker = new Faker<User>()
                .RuleFor(u => u.UserId, f => f.IndexFaker + 1)
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
                FirstName = "Basem",
                LastName = "Ojbuduk",
                Email = "basem@yahoo.com",
                Role = UserRole.User, // (1)
                Address = "123 Main Street",
                PostalCode = "10001",
                City = "New York",
                Region = "NY",
                PhoneNumber = "(555) 555-1212",
            };

            var user = userFaker.Generate();
            moqRepository.Setup(r => r.GetById(user.UserId)).ReturnsAsync(user);
            moqRepository.Setup(r => r.Update(It.IsAny<User>(), user.UserId)).ReturnsAsync(user);

            var controller = new UsersController(moqDataContext.Object, moqRepository.Object);

            // Act
            var result = await controller.UpdateUser(user.UserId, userToUpdate);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okObjResult = result as OkObjectResult;
            okObjResult.Should().NotBeNull();
        }


        [Fact]
        public async Task DeleteUser_ReturnsOk()
        {
            // Arrange
            var moqRepository = new Mock<IUserRepository>();
            var moqDataContext = new Mock<DataContext>(new DbContextOptions<DataContext>());

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
            var userId = generateFakeUser.UserId;

            moqRepository.Setup(r => r.GetById(userId)).ReturnsAsync(generateFakeUser);
            moqRepository.Setup(r => r.Delete(userId)).ReturnsAsync(generateFakeUser);

            var controller = new UsersController(moqDataContext.Object, moqRepository.Object);

            // Act
            var result = await controller.DeleteUser(userId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

    }
}
