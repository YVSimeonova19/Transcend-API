using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transcend.BLL.Contracts;
using Transcend.Common.Models.Carrier;
using Transcend.Common.Models.Order;
using Transcend.Common.Utilities;

namespace Transcend.PL.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Customer")]
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
    public async Task<ActionResult<List<OrderVM>>> GetAllOrdersAsync()
    {
        return await orderService.GetAllOrdersByIdAsync(currentUser.UserId);
    }

    [HttpGet("TrackOrder/{id}")]
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

}
