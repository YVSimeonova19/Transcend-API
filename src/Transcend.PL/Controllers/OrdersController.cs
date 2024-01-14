using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transcend.BLL.Contracts;
using Transcend.Common.Models.Order;
using Transcend.Common.Utilities;

namespace Transcend.PL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService orderService;
    private readonly ICurrentUser currentUser;

    // Add dependency injections
    public OrdersController(IOrderService orderService, ICurrentUser currentUser)
    {
        this.orderService = orderService;
        this.currentUser = currentUser;
    }

    // Place an order asyncronously (Only available for customers)
    [HttpPost]
    [Authorize(Roles = "Customer")]
    public async Task<ActionResult<Response>> PlaceOrderAsync([FromBody] OrderIM orderIM)
    {
        // Create the order
        await orderService.CreateOrderAsync(orderIM, currentUser.UserId);

        // Confirm the order creation
        return this.Ok(
            new Response
            {
                Status = "Order placed successfully",
                Message = "Your order has been placed successfully!"
            });
    }

    // Get a list of all orders that belong to the current user asyncronously (For customers)
    [HttpGet("GetAllOrders")]
    [Authorize(Roles = "Customer")]
    public async Task<ActionResult<List<OrderVM>>> GetAllOrdersAsync()
    {
        return await orderService.GetAllOrdersByIdAsync(currentUser.UserId);
    }

    // Get the information for a certain order asyncronously (For customers)
    [HttpGet("TrackOrder/{id}")]
    [Authorize(Roles = "Customer")]
    public async Task<ActionResult<OrderVM>> GetOrderStatusAsync(int id)
    {
        // Retrieve the order information
        var order = await orderService.GetOrderInfoByIdAsync(id);

        // Check if the order exists
        if (order == null)
            return this.BadRequest(new Response
            {
                Status = "Order does not exist",
                Message = "This order does not exist!"
            });

        // Check if the order belongs to the current user
        if (order.UserPlaceId != currentUser.UserId)
            return this.Forbid();

        // Return the order details
        return this.Ok(order);
    }

    // Get a list of all orders that belong to the current user asyncronously (For carriers)
    [HttpGet("GetAllOrdersCarrier")]
    [Authorize(Roles = "Carrier")]
    public async Task<ActionResult<List<OrderVM>>> GetAllOrdersCarrierAsync()
    {
        return await orderService.GetAllOrdersByIdCarrierAsync(currentUser.UserId);
    }

    // Get the information for a certain order asyncronously (For carriers)
    [HttpGet("TrackOrderCarrier/{id}")]
    [Authorize(Roles = "Carrier")]
    public async Task<ActionResult<OrderVM>> GetOrderStatusCarrierAsync(int id)
    {
        // Retrieve the order information
        var order = await orderService.GetOrderInfoByIdAsync(id);

        // Check if the order exists
        if (order == null)
            return this.BadRequest(new Response
            {
                Status = "Order does not exist",
                Message = "This order does not exist!"
            });

        // Check if the order belongs to the current user
        if (order.CarrierId != currentUser.UserId)
            return this.Forbid();

        // Return the order details
        return this.Ok(order);
    }

    // Update an order by its id asyncronously (Only available for carriers)
    [HttpPatch("{id}")]
    [Authorize(Roles = "Carrier")]
    public async Task<ActionResult<OrderVM>> UpdateOrder([FromBody]OrderUM orderUM, int id)
    {
        // Check if the order exists
        if (!await orderService.CheckIfOrderExistsByIdAsync(id))
            return NotFound();

        // Get the order details and check if it belongs to the current user
        var order = await orderService.GetOrderInfoByIdAsync(id);
        if (order.CarrierId != currentUser.UserId)
            return this.Forbid();

        // Update the order
        return await orderService.UpdateOrderAsync(id, orderUM);
    }
}
