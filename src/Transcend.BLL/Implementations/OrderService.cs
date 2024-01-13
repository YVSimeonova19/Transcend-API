using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transcend.BLL.Contracts;
using Transcend.Common.Models.Order;
using Transcend.DAL.Data;
using Transcend.DAL.Models;

namespace Transcend.BLL.Implementations;

internal class OrderService : IOrderService
{
    private readonly IMapper mapper;
    private readonly TranscendDBContext dbContext;

    public OrderService(IMapper mapper, TranscendDBContext dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }

    public async Task CreateOrderAsync(OrderIM orderIM, string userId)
    {
        var order = this.mapper.Map<Order>(orderIM);

        order.UserPlaceId = userId;

        this.dbContext.Orders.Add(order);

        await this.dbContext.SaveChangesAsync();
    }

    public async Task<List<OrderVM>> GetAllOrdersByIdAsync(string userId)
    {
        return await this.dbContext.Orders.Where(o => o.UserPlaceId == userId).ProjectTo<OrderVM>(this.mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<OrderVM> GetOrderInfoByIdAsync(int orderId)
    {
        return await dbContext.Orders.Where(o => o.Id == orderId).ProjectTo<OrderVM>(this.mapper.ConfigurationProvider).FirstOrDefaultAsync();
    }
}
