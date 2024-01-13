using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Transcend.BLL.Contracts;
using Transcend.Common.Models.Carrier;
using Transcend.Common.Models.Order;
using Transcend.Common.Utilities;
using Transcend.DAL.Models;

namespace Transcend.PL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService orderService;
    private readonly ICurrentUser currentUser;

    public OrdersController(IOrderService orderService, ICurrentUser currentUser)
    {
        this.orderService = orderService;
        this.currentUser = currentUser;
    }

    [HttpPost]
    [Authorize(Roles = "Customer")]
    public async Task<ActionResult<Response>> PlaceOrderAsync([FromBody] OrderIM orderIM)
    {
        await orderService.CreateOrderAsync(orderIM, currentUser.UserId);

        return this.Ok(
            new Response
            {
                Status = "Order placed successfully",
                Message = "Your order has been placed successfully!"
            });
    }

    [HttpGet("GetAllOrders")]
    [Authorize(Roles = "Customer")]
    public async Task<ActionResult<List<OrderVM>>> GetAllOrdersAsync()
    {
        return await orderService.GetAllOrdersByIdAsync(currentUser.UserId);
    }

    [HttpGet("TrackOrder/{id}")]
    [Authorize(Roles = "Customer")]
    public async Task<ActionResult<OrderVM>> GetOrderStatusAsync(int id)
    {
        var order = await orderService.GetOrderInfoByIdAsync(id);

        if (order == null)
            return this.BadRequest(new Response
            {
                Status = "Order does not exist",
                Message = "This order does not exist!"
            });

        if (order.UserPlaceId != currentUser.UserId)
            return this.Forbid();

        return this.Ok(order);
    }

    [HttpGet("GetAllOrdersCarrier")]
    [Authorize(Roles = "Carrier")]
    public async Task<ActionResult<List<OrderVM>>> GetAllOrdersCarrierAsync()
    {
        return await orderService.GetAllOrdersByIdCarrierAsync(currentUser.UserId);
    }

    [HttpGet("TrackOrderCarrier/{id}")]
    [Authorize(Roles = "Carrier")]
    public async Task<ActionResult<OrderVM>> GetOrderStatusCarrierAsync(int id)
    {
        var order = await orderService.GetOrderInfoByIdAsync(id);

        if (order == null)
            return this.BadRequest(new Response
            {
                Status = "Order does not exist",
                Message = "This order does not exist!"
            });

        if (order.CarrierId != currentUser.UserId)
            return this.Forbid();

        return this.Ok(order);
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = "Carrier")]
    public async Task<ActionResult<OrderVM>> UpdateOrder([FromBody]OrderUM orderUM, int id)
    {
        if (!await orderService.CheckIfOrderExistsByIdAsync(id))
            return NotFound();

        var order = await orderService.GetOrderInfoByIdAsync(id);
        if (order.CarrierId != currentUser.UserId)
            return this.Forbid();

        return await orderService.UpdateOrderAsync(id, orderUM);
    }
}
