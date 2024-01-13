using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transcend.BLL.Contracts;
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
}
