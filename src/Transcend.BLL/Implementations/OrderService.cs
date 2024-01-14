using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Transcend.BLL.Contracts;
using Transcend.Common.Models.Order;
using Transcend.DAL.Data;
using Transcend.DAL.Models;

namespace Transcend.BLL.Implementations;

internal class OrderService : IOrderService
{
    private readonly IMapper mapper;
    private readonly TranscendDBContext dbContext;

    // Add dependency injections
    public OrderService(IMapper mapper, TranscendDBContext dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }

    // Add a new order to the DB asyncronously
    public async Task CreateOrderAsync(OrderIM orderIM, string userId)
    {
        // Create a new order
        var order = this.mapper.Map<Order>(orderIM);

        // Make the id of the currently logged user the userPlaceId
        order.UserPlaceId = userId;

        // Add the order to the orders dbSet
        this.dbContext.Orders.Add(order);

        await this.dbContext.SaveChangesAsync();
    }

    // Retrieve all orders thet belong to a certain customer from the DB asyncronously
    public async Task<List<OrderVM>> GetAllOrdersByIdAsync(string userId)
    {
        return await this.dbContext.Orders
            .Where(o => o.UserPlaceId == userId)
            .ProjectTo<OrderVM>(this.mapper.ConfigurationProvider)
            .ToListAsync();
    }

    // Get the information of an order by its id asyncronously
    public async Task<OrderVM> GetOrderInfoByIdAsync(int orderId)
    {
        return await dbContext.Orders
            .Where(o => o.Id == orderId)
            .ProjectTo<OrderVM>(this.mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    // Retrieve all orders thet belong to a certain carrier from the DB asyncronously
    public async Task<List<OrderVM>> GetAllOrdersByIdCarrierAsync(string carrierId)
    {
        return await this.dbContext.Orders
            .Where(o => o.CarrierId == carrierId)
            .ProjectTo<OrderVM>(this.mapper.ConfigurationProvider)
            .ToListAsync();
    }

    // Check if an order exists by its id asyncronously
    public async Task<bool> CheckIfOrderExistsByIdAsync(int id)
    {
        return this.dbContext.Orders.Where(o => o.Id == id).Count() != 0;
    }

    // Update an orders information asyncronously
    public async Task<OrderVM> UpdateOrderAsync(int id, OrderUM orderUM)
    {
        // Get the order
        var order = await this.dbContext.Orders.FindAsync(id);

        // Update status if changed
        if (orderUM.Status != null)
            order.Status = orderUM.Status;

        // Update expected delivery date if changed
        if (orderUM.ExpectedDeliveryDate != null)
            order.ExpectedDeliveryDate = orderUM.ExpectedDeliveryDate;

        this.dbContext.Orders.Update(order);
        await this.dbContext.SaveChangesAsync();

        return this.mapper.Map<OrderVM>(order);
    }
}
