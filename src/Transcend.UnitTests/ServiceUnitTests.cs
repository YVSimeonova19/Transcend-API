using NUnit.Framework;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Transcend.BLL.Contracts;
using Transcend.Common.Models.Carrier;
using Transcend.Common.Models.User;
using Transcend.DAL.Data;
using Transcend.DAL.Models;
using Transcend.BLL.Implementations;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Transcend.PL.Mapping;
using Transcend.Common.Models.Order;

namespace Transcend.BLL.Tests;

public class UnitTests
{
    private TranscendDBContext dbContext;
    private IMapper mapper;
    private IOrderService orderService;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<TranscendDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        this.dbContext = new TranscendDBContext(options);

        this.mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        }).CreateMapper();

        this.orderService = new OrderService(mapper, dbContext);
    }

    [Test]
    public async Task TestCreateOrderAsync()
    {
        // Arrange
        string name = "Order";
        string carrierId = "carrierid";
        var orderIM = new OrderIM { Name = name, CarrierId = carrierId };

        // Act
        await orderService.CreateOrderAsync(orderIM, "userid");

        // Assert
        Assert.True(this.dbContext.Orders.ToList().Count == 1);
    }

    [Test]
    public async Task TestGetAllOrdersByIdAsync()
    {
        // Arrange
        var orders = new List<Order> { new Order{ UserPlaceId = "userid" }, new Order { UserPlaceId = "id" } };
        await this.dbContext.AddRangeAsync(orders);
        await this.dbContext.SaveChangesAsync();

        // Act
        var result = await this.orderService.GetAllOrdersByIdAsync("userid");

        // Assert
        Assert.That(result.Count, Is.EqualTo(1));
    }

    [Test]
    public async Task TestCheckIfOrderExistsByIdAsync()
    {
        // Arrange
        int orderId = 1;
        var orders = new List<Order> { new Order { Id = 1 }, new Order { Id = 2 } };
        await this.dbContext.AddRangeAsync(orders);
        await this.dbContext.SaveChangesAsync();

        // Act
        var result = await this.orderService.CheckIfOrderExistsByIdAsync(orderId);

        // Assert
        Assert.That(result, Is.EqualTo(true));
    }
}
