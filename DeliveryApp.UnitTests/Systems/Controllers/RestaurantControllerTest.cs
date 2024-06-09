using AutoMapper;
using Bogus;
using DeliveryApp.API.Controllers;
using DeliveryApp.API.DTOs;
using DeliveryApp.API.Repository;
using DeliveryApp.API.Services;
using DeliveryAppBackend.DataLayers.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DeliveryApp.UnitTests.Systems.Controllers
{
    public class RestaurantControllerTest
    {
        [Fact]
        public async Task GetRestaurants_ReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IRestaurantRepository>();
            var moqMapper = new Mock<IMapper>();
            var moqFile = new Mock<IFileService>();
            var restaurantFaker = new Faker<Restaurant>()
                .RuleFor(r => r.RestaurantId, f => f.PickRandom(1, 1000))
                .RuleFor(r => r.Name, f => f.Name.FullName())
                /*                .RuleFor(r => r.Description, f => f.Lorem.Paragraph(100))*/
                .RuleFor(r => r.Address, f => f.Address.FullAddress())
                .RuleFor(r => r.Email, f => f.Person.Email)
                .RuleFor(r => r.PhoneNumber, f => f.Phone.ToString())
                /*                .RuleFor(r => r.DeliveryFee, f => f.Random.Decimal(1, 10))*/
                .RuleFor(r => r.OperatingHours, f => f.Date.ToString());

            var data = restaurantFaker.Generate(10);
            mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(data);

            var controller = new RestaurantsController(mockRepository.Object, moqMapper.Object, moqFile.Object);

            // Act
            var result = await controller.GetRestaurants();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var restaurants = (result as OkObjectResult).Value.Should().BeAssignableTo<IEnumerable<Restaurant>>().Subject;
            restaurants.Should().HaveCount(10);
        }

        [Fact]
        public async Task GetSingleRestaurant_ReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IRestaurantRepository>();
            var moqMapper = new Mock<IMapper>();
            var moqFile = new Mock<IFileService>();
            var restaurantFaker = new Faker<Restaurant>()
                .RuleFor(r => r.RestaurantId, f => f.PickRandom(1, 1000))
                .RuleFor(r => r.Name, f => f.Name.FullName())
                /*                .RuleFor(r => r.Description, f => f.Lorem.Paragraph(100))*/
                .RuleFor(r => r.Address, f => f.Address.FullAddress())
                .RuleFor(r => r.Email, f => f.Person.Email)
                .RuleFor(r => r.PhoneNumber, f => f.Phone.ToString())
                /*                .RuleFor(r => r.DeliveryFee, f => f.Random.Decimal(1, 10))*/
                .RuleFor(r => r.OperatingHours, f => f.Date.ToString());

            var restaurant = restaurantFaker.Generate();
            mockRepository.Setup(r => r.GetById(restaurant.RestaurantId)).ReturnsAsync(restaurant);

            var controller = new RestaurantsController(mockRepository.Object, moqMapper.Object, moqFile.Object);

            // Act
            var result = await controller.GetSingleRestaurant(restaurant.RestaurantId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var retrievedRestaurant = (result as OkObjectResult).Value.Should().BeAssignableTo<Restaurant>().Subject;
            retrievedRestaurant.RestaurantId.Should().Be(restaurant.RestaurantId);
        }

        [Fact]
        public async Task AddRestaurant_ReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IRestaurantRepository>();
            var moqMapper = new Mock<IMapper>();
            var moqFile = new Mock<IFileService>();
            var restaurantDto = new RestaurantDTO(); // Provide necessary data for RestaurantDTO

            var restaurantFaker = new Faker<Restaurant>()
                .RuleFor(r => r.RestaurantId, f => f.Random.Number(1, 1000))
                .RuleFor(r => r.Name, f => f.Name.FullName())
                /*                .RuleFor(r => r.Description, f => f.Lorem.Paragraph(100))*/
                .RuleFor(r => r.Address, f => f.Address.FullAddress())
                .RuleFor(r => r.Email, f => f.Person.Email)
                .RuleFor(r => r.PhoneNumber, f => f.Phone.ToString())
                /*                .RuleFor(r => r.DeliveryFee, f => f.Random.Decimal(1, 10))*/
                .RuleFor(r => r.OperatingHours, f => f.Date.ToString());

            var restaurant = restaurantFaker.Generate();
            mockRepository.Setup(r => r.Add(It.IsAny<Restaurant>())).ReturnsAsync(restaurant);

            var controller = new RestaurantsController(mockRepository.Object, moqMapper.Object, moqFile.Object);

            // Act
            var result = await controller.AddRestaurants(restaurantDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UpdateRestaurant_ReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IRestaurantRepository>();
            var moqMapper = new Mock<IMapper>();
            var moqFile = new Mock<IFileService>();
            var restaurantDto = new RestaurantDTO(); // Provide necessary data for RestaurantDTO

            var restaurantFaker = new Faker<Restaurant>()
                .RuleFor(r => r.RestaurantId, f => f.Random.Number(1, 1000))
                .RuleFor(r => r.Name, f => f.Name.FullName())
                /*                .RuleFor(r => r.Description, f => f.Lorem.Paragraph(100))*/
                .RuleFor(r => r.Address, f => f.Address.FullAddress())
                .RuleFor(r => r.Email, f => f.Person.Email)
                .RuleFor(r => r.PhoneNumber, f => f.Phone.ToString())
                /*                .RuleFor(r => r.DeliveryFee, f => f.Random.Decimal(1, 10))*/
                .RuleFor(r => r.OperatingHours, f => f.Date.ToString());

            var restaurant = restaurantFaker.Generate();
            // Set up GetById to return the restaurant
            mockRepository.Setup(r => r.GetById(restaurant.RestaurantId)).ReturnsAsync(restaurant);

            var controller = new RestaurantsController(mockRepository.Object, moqMapper.Object, moqFile.Object);

            // Act
            var result = await controller.UpdateRestaurants(restaurantDto, restaurant.RestaurantId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task DeleteRestaurant_ReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IRestaurantRepository>();
            var moqMapper = new Mock<IMapper>();
            var moqFile = new Mock<IFileService>();
            var restaurantFaker = new Faker<Restaurant>()
                .RuleFor(r => r.RestaurantId, f => f.Random.Number(1, 1000))
                .RuleFor(r => r.Name, f => f.Name.FullName())
                /*                .RuleFor(r => r.Description, f => f.Lorem.Paragraph(100))*/
                .RuleFor(r => r.Address, f => f.Address.FullAddress())
                .RuleFor(r => r.Email, f => f.Person.Email)
                .RuleFor(r => r.PhoneNumber, f => f.Phone.ToString())
                /*                .RuleFor(r => r.DeliveryFee, f => f.Random.Decimal(1, 10))*/
                .RuleFor(r => r.OperatingHours, f => f.Date.ToString());

            var restaurant = restaurantFaker.Generate();
            mockRepository.Setup(r => r.Delete(restaurant.RestaurantId)).ReturnsAsync(restaurant);

            var controller = new RestaurantsController(mockRepository.Object, moqMapper.Object, moqFile.Object);

            // Act
            var result = await controller.DeleteRestaurants(restaurant.RestaurantId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
