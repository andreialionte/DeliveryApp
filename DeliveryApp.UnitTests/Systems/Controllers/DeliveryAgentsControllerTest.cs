using Bogus;
using DeliveryApp.API.Controllers;
using DeliveryApp.API.DataLayers.Entities.Enums;
using DeliveryApp.API.DTOs;
using DeliveryApp.API.Repository;
using DeliveryAppBackend.DataLayers.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DeliveryApp.UnitTests.Systems.Controllers
{
    public class DeliveryAgentsControllerTest
    {
        [Fact]
        public async Task GetDeliveryAgents_ReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IDeliveryAgentsRepository>();
            var agentFaker = new Faker<DeliveryAgent>()
                .RuleFor(a => a.DeliveryAgentId, f => f.PickRandom(1, 1000))
                .RuleFor(a => a.FirstName, f => f.Name.FirstName())
                .RuleFor(a => a.LastName, f => f.Name.LastName())
                .RuleFor(a => a.Email, f => f.Person.Email)
                .RuleFor(a => a.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(a => a.Status, f => f.PickRandom<DeliveryAgentStatus>());

            var agents = agentFaker.Generate(10);
            mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(agents);

            var controller = new DeliveryAgentsController(mockRepository.Object);

            // Act
            var result = await controller.GetDeliveryAgents();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var retrievedAgents = (result as OkObjectResult).Value.Should().BeAssignableTo<IEnumerable<DeliveryAgent>>().Subject;
            retrievedAgents.Should().HaveCount(10);
        }

        [Fact]
        public async Task GetSingleDeliveryAgent_ReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IDeliveryAgentsRepository>();
            var agentFaker = new Faker<DeliveryAgent>()
                .RuleFor(a => a.DeliveryAgentId, f => f.PickRandom(1, 1000))
                .RuleFor(a => a.FirstName, f => f.Name.FirstName())
                .RuleFor(a => a.LastName, f => f.Name.LastName())
                .RuleFor(a => a.Email, f => f.Person.Email)
                .RuleFor(a => a.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(a => a.Status, f => f.PickRandom<DeliveryAgentStatus>());

            var agent = agentFaker.Generate();
            mockRepository.Setup(repo => repo.GetById(agent.DeliveryAgentId)).ReturnsAsync(agent);

            var controller = new DeliveryAgentsController(mockRepository.Object);

            // Act
            var result = await controller.GetSingleDeliveryAgent(agent.DeliveryAgentId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var retrievedAgent = (result as OkObjectResult).Value.Should().BeAssignableTo<DeliveryAgent>().Subject;
            retrievedAgent.DeliveryAgentId.Should().Be(agent.DeliveryAgentId);
        }

        [Fact]
        public async Task AddDeliveryAgent_ReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IDeliveryAgentsRepository>();
            var agentDto = new DeliveryAgentDTO(); // Provide necessary data for DeliveryAgentDTO

            var agentFaker = new Faker<DeliveryAgent>()
                .RuleFor(a => a.DeliveryAgentId, f => f.Random.Number(1, 1000))
                .RuleFor(a => a.FirstName, f => f.Name.FirstName())
                .RuleFor(a => a.LastName, f => f.Name.LastName())
                .RuleFor(a => a.Email, f => f.Person.Email)
                .RuleFor(a => a.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(a => a.Status, f => f.PickRandom<DeliveryAgentStatus>());

            var agent = agentFaker.Generate();
            mockRepository.Setup(repo => repo.Add(It.IsAny<DeliveryAgent>())).ReturnsAsync(agent);

            var controller = new DeliveryAgentsController(mockRepository.Object);

            // Act
            var result = await controller.AddDeliveryAgent(agentDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UpdateDeliveryAgent_ReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IDeliveryAgentsRepository>();

            var existingAgentId = 1;
            var existingAgent = new DeliveryAgent
            {
                DeliveryAgentId = existingAgentId,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "123-456-7890",
                Status = DeliveryAgentStatus.Available
            };

            var updatedAgentDto = new DeliveryAgentDTO
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                PhoneNumber = "987-654-3210",
                Status = DeliveryAgentStatus.Busy
            };

            mockRepository.Setup(repo => repo.GetById(existingAgentId)).ReturnsAsync(existingAgent);

            var controller = new DeliveryAgentsController(mockRepository.Object);

            // Act
            var result = await controller.UpdateDeliveryAgent(existingAgentId, updatedAgentDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeOfType<DeliveryAgent>();
            var updatedAgent = okResult.Value as DeliveryAgent;
            updatedAgent.Should().NotBeNull();
            updatedAgent.FirstName.Should().Be(updatedAgentDto.FirstName);
            updatedAgent.LastName.Should().Be(updatedAgentDto.LastName);
            updatedAgent.Email.Should().Be(updatedAgentDto.Email);
            updatedAgent.PhoneNumber.Should().Be(updatedAgentDto.PhoneNumber);
            updatedAgent.Status.Should().Be(updatedAgentDto.Status);
        }



        [Fact]
        public async Task DeleteDeliveryAgent_ReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IDeliveryAgentsRepository>();
            var agentFaker = new Faker<DeliveryAgent>()
                .RuleFor(a => a.DeliveryAgentId, f => f.Random.Number(1, 1000))
                .RuleFor(a => a.FirstName, f => f.Name.FirstName())
                .RuleFor(a => a.LastName, f => f.Name.LastName())
                .RuleFor(a => a.Email, f => f.Person.Email)
                .RuleFor(a => a.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(a => a.Status, f => f.PickRandom<DeliveryAgentStatus>());

            var agent = agentFaker.Generate();
            mockRepository.Setup(repo => repo.Delete(agent.DeliveryAgentId)).ReturnsAsync(agent);

            var controller = new DeliveryAgentsController(mockRepository.Object);

            // Act
            var result = await controller.DeleteDeliveryAgent(agent.DeliveryAgentId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okresult = result as OkObjectResult;
            okresult.Should().NotBeNull();
        }
    }
}
