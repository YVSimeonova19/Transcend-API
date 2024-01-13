﻿using AutoMapper;
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

    public async Task<List<OrderVM>> GetAllOrdersByIdCarrierAsync(string carrierId)
    {
        return await this.dbContext.Orders.Where(o => o.CarrierId == carrierId).ProjectTo<OrderVM>(this.mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<bool> CheckIfOrderExistsByIdAsync(int id)
    {
        return this.dbContext.Orders.Where(o => o.Id == id).Count() != 0;
    }

    public async Task<OrderVM> UpdateOrderAsync(int id, OrderUM orderUM)
    {
        var order = await this.dbContext.Orders.FindAsync(id);

        if (orderUM.Status != null)
            order.Status = orderUM.Status;

        if (orderUM.ExpectedDeliveryDate != null)
            order.ExpectedDeliveryDate = orderUM.ExpectedDeliveryDate;

        this.dbContext.Orders.Update(order);
        await this.dbContext.SaveChangesAsync();

        return this.mapper.Map<OrderVM>(order);
    }
}
