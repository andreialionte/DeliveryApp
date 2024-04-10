using Bogus;
using DeliveryApp.API.DataLayers;
using DeliveryAppBackend.DataLayers.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.UnitTests.Systems.Controllers
{
    internal class MenuItemsControllerTest
    {
        [Fact]
        public async Task GetMenuItems_ReturnsOk()
        {
            var db = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("DeliveryApp").Options;
            var context = new DataContext(db);

            var fakeMenuItems = new Faker<MenuItem>().RuleFor(r => r.MenuItemId, f => f.PickRandom(1, 1000))
            .RuleFor(r => r.))
        }
    }
}
