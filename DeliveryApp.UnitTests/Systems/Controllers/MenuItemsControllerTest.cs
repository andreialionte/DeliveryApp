using Bogus;
using DeliveryApp.API.Controllers;
using DeliveryApp.API.DataLayers;
using DeliveryApp.API.DataLayers.Entities.Enums;
using DeliveryApp.API.DTOs;
using DeliveryApp.API.Repository;
using DeliveryAppBackend.DataLayers.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DeliveryApp.UnitTests.Systems.Controllers
{
    public class MenuItemsControllerTest
    {
        [Fact]
        public async Task GetMenuItems_ReturnsOk()
        {
            // ARRANGE
            var moqRepository = new Mock<IMenuItemRepository>();
            var moqDataContext = new Mock<DataContext>(new DbContextOptions<DataContext>());

            var faker = new Faker<MenuItem>()
                .RuleFor(m => m.MenuItemId, f => f.IndexFaker + 1)
                .RuleFor(m => m.RestaurantId, f => f.Random.Number(1, 1000))
                .RuleFor(m => m.Name, f => f.Commerce.ProductName())
                .RuleFor(m => m.Description, f => f.Commerce.ProductDescription())
                .RuleFor(m => m.Price, f => f.Random.Decimal(2, 18))
                .RuleFor(m => m.Category, f => f.PickRandom<MenuItemCategory>());

            var fakeMenuItems = faker.Generate(10);

            moqRepository.Setup(r => r.GetAll()).ReturnsAsync(fakeMenuItems);
            var controller = new MenuItemsController(moqDataContext.Object, moqRepository.Object);

            // ACT
            var result = await controller.GetMenuItems();

            // ASSERT
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            var menuItems = okResult.Value.Should().BeAssignableTo<IEnumerable<MenuItem>>().Subject;
            menuItems.Should().HaveCount(10);
        }

        [Fact]
        public async Task GetSingleMenuItem_ReturnsOk()
        {
            //Arrange
            var moqRepository = new Mock<IMenuItemRepository>();
            var moqDataContext = new Mock<DataContext>(new DbContextOptions<DataContext>());

            var faker = new Faker<MenuItem>()
    .RuleFor(m => m.MenuItemId, f => f.IndexFaker + 1)
    .RuleFor(m => m.RestaurantId, f => f.Random.Number(1, 1000))
    .RuleFor(m => m.Name, f => f.Commerce.ProductName())
    .RuleFor(m => m.Description, f => f.Commerce.ProductDescription())
    .RuleFor(m => m.Price, f => f.Random.Decimal(2, 18))
    .RuleFor(m => m.Category, f => f.PickRandom<MenuItemCategory>());
            var fakeMenuItems = faker.Generate();

            moqRepository.Setup(r => r.GetById(fakeMenuItems.MenuItemId)).ReturnsAsync(fakeMenuItems);

            var controller = new MenuItemsController(moqDataContext.Object, moqRepository.Object);

            //ACT
            var result = await controller.GetSingleMenuItem(fakeMenuItems.MenuItemId);

            //Assert
            result.Should().BeOfType<OkObjectResult>();

        }

        [Fact]
        public async Task AddMenuItems_ReturnsOk()
        {
            // ARRANGE
            var moqRepository = new Mock<IMenuItemRepository>();
            var moqDataContext = new Mock<DataContext>(new DbContextOptions<DataContext>());
            var menuItemDto = new MenuItemDTO();

            var faker = new Faker<MenuItem>()
                .RuleFor(m => m.MenuItemId, f => f.IndexFaker + 1)
                .RuleFor(m => m.RestaurantId, f => f.Random.Number(1, 1000))
                .RuleFor(m => m.Name, f => f.Commerce.ProductName())
                .RuleFor(m => m.Description, f => f.Commerce.ProductDescription())
                .RuleFor(m => m.Price, f => f.Random.Decimal(2, 18))
                .RuleFor(m => m.Category, f => f.PickRandom<MenuItemCategory>());
            var fakeMenuItem = faker.Generate();

            moqRepository.Setup(r => r.Add(It.IsAny<MenuItem>())).ReturnsAsync(fakeMenuItem);

            var controller = new MenuItemsController(moqDataContext.Object, moqRepository.Object);

            // ACT
            var result = await controller.AddMenuItems(menuItemDto);

            // ASSERT
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull(); // Check if the result is not null
            /*            okResult.Value.Should().BeEquivalentTo(fakeMenuItem); // Compare the result value with the fake menu item*/
        }


        [Fact]
        public async Task UpdateMenuItems_ReturnsOk()
        {
            //here we need to to with a MenuItemDTO to create and update it with that DTO and create a FakeMenuItem.
            //Arrange
            var moqRepository = new Mock<IMenuItemRepository>();
            var moqDataContext = new Mock<DataContext>(new DbContextOptions<DataContext>());

            var menuItem = new MenuItemDTO();

            var faker = new Faker<MenuItem>()
               .RuleFor(m => m.MenuItemId, f => f.IndexFaker + 1)
               .RuleFor(m => m.RestaurantId, f => f.Random.Number(1, 1000))
               .RuleFor(m => m.Name, f => f.Commerce.ProductName())
               .RuleFor(m => m.Description, f => f.Commerce.ProductDescription())
               .RuleFor(m => m.Price, f => f.Random.Decimal(2, 18))
               .RuleFor(m => m.Category, f => f.PickRandom<MenuItemCategory>());
            var fakeMenuItem = faker.Generate();

            moqRepository.Setup(r => r.Update(fakeMenuItem, fakeMenuItem.MenuItemId)).ReturnsAsync(fakeMenuItem);

            var controller = new MenuItemsController(moqDataContext.Object, moqRepository.Object);
            //Act
            var result = await controller.UpdateMenuItems(menuItem, fakeMenuItem.MenuItemId);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Should().Be(okResult);
            okResult.Should().BeEquivalentTo(fakeMenuItem);
            okResult.Should().NotBeNull();

        }

        [Fact]
        public async Task DeleteMenuItems_ReturnsOk()
        {
            // ARRANGE
            var moqRepository = new Mock<IMenuItemRepository>();
            var moqDataContext = new Mock<DataContext>(new DbContextOptions<DataContext>());

            var faker = new Faker<MenuItem>()
                .RuleFor(m => m.MenuItemId, f => f.IndexFaker + 1)
                .RuleFor(m => m.RestaurantId, f => f.Random.Number(1, 1000))
                .RuleFor(m => m.Name, f => f.Commerce.ProductName())
                .RuleFor(m => m.Description, f => f.Commerce.ProductDescription())
                .RuleFor(m => m.Price, f => f.Random.Decimal(2, 18))
                .RuleFor(m => m.Category, f => f.PickRandom<MenuItemCategory>());
            var fakeMenuItem = faker.Generate();

            moqRepository.Setup(r => r.Delete(fakeMenuItem.MenuItemId)).ReturnsAsync(fakeMenuItem);

            var controller = new MenuItemsController(moqDataContext.Object, moqRepository.Object);

            // ACT
            var result = await controller.DeleteMenuItems(fakeMenuItem.MenuItemId);

            // ASSERT
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();

            // Compare only the message property
            okResult.Value.Should().BeEquivalentTo(new { message = "MenuItem was added" });
        }

    }
}