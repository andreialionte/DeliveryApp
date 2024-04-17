using Bogus;
using DeliveryApp.API.Controllers;
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
            var okObject = result as OkObjectResult;
            okObject.Should().NotBeNull();
            okObject.Should().BeOfType<OrderItem>();

        }

        [Fact]
        public async Task GetSingleOrderItem()
        {
            var moqRepo = new Mock<IOrderItemRepository>();


        }

        [Fact]
        public async Task AddOrderItem_ReturnsOk()
        {
            //Arrange
            var moqRepo = new Mock<IOrderItemRepository>();

            var faker = new Faker<OrderItem>().RuleFor(r => r.OrderItemId, f => f.PickRandom(1, 1000))
     .RuleFor(r => r.Quantity, f => f.PickRandom(1, 100));
            var orderItem = faker.Generate();
            moqRepo.Setup(repo => repo.Add(orderItem)).ReturnsAsync(orderItem);

            var controller = new OrderItemsController(moqRepo.Object);

            //ACT
            var result = await controller.AddOrderItems();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var OkResult = result as OkObjectResult;
            OkResult.Should().NotBeNull();
            OkResult.Should().BeOfType<OrderItem>();
            OkResult.Should().BeEquivalentTo(orderItem);

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
