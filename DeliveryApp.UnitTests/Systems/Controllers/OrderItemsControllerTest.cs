using Bogus;
using DeliveryApp.API.Controllers;
using DeliveryApp.API.DTOs;
using DeliveryApp.API.Repository;
using DeliveryAppBackend.DataLayers.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DeliveryApp.UnitTests.Systems.Controllers
{
    public class OrderItemsControllerTest
    {

        [Fact]
        public async Task GetOrderItems_ReturnsOk()
        {
            //ARRANGE
            var moqRepo = new Mock<IOrderItemRepository>();

            var faker = new Faker<OrderItem>().RuleFor(r => r.OrderItemId, f => f.PickRandom(1, 1000))
                .RuleFor(r => r.Quantity, f => f.PickRandom(1, 100));

            var orderItem = faker.Generate(10);
            moqRepo.Setup(r => r.GetAll()).ReturnsAsync(orderItem);

            var controller = new OrderItemsController(moqRepo.Object);
            //Result
            var result = await controller.GetOrderItems();

            //Asset
            result.Should().BeOfType<OkObjectResult>();
                var okResult = result as OkObjectResult;
        }

        [Fact]
        public async Task GetSingleOrderItem_ReturnsOk()
        {
            //ARRANGE
            var moqRepo = new Mock<IOrderItemRepository>();

            var faker = new Faker<OrderItem>().RuleFor(r => r.OrderItemId, f => f.PickRandom(1, 1000))
                .RuleFor(r => r.Quantity, f => f.PickRandom(1, 100))
                .RuleFor(r => r.TotalPrice, f => f.Random.Decimal(1, 100));

            var orderItem = faker.Generate();
            
            /*/*moqRepo.Setup(repo => repo.GetById(orderItem.MenuItemId.Value)).R#1#eturnsAsync(orderItem);*/
            moqRepo.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(orderItem);


            var controller = new OrderItemsController(moqRepo.Object);

            //Act
            var result = await controller.GetSingleOrderItem(orderItem.OrderItemId);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task AddOrderItem_ReturnsOk()
        {
            //Arrange
            var moqRepo = new Mock<IOrderItemRepository>();
            var orderItemDto = new OrderItemDTO();

            var faker = new Faker<OrderItem>().
                RuleFor(r => r.OrderItemId, f => f.PickRandom(1, 1000))
     .          RuleFor(r => r.Quantity, f => f.PickRandom(1, 100));
            var orderItem = faker.Generate();
            moqRepo.Setup(repo => repo.Add(orderItem)).ReturnsAsync(orderItem);

            var controller = new OrderItemsController(moqRepo.Object);

            //ACT
            var result = await controller.AddOrderItems(orderItemDto);

            //Assert
            result.Should().BeOfType<OkObjectResult>();

        }

        [Fact]
        public async Task UpdateOrderItem()
        {

        }

        [Fact]
        public async Task DeleteOrderItem()
        {

        }
    }
}
