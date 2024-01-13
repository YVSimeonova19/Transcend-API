using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transcend.BLL.Contracts;
using Transcend.Common.Models.Carrier;

namespace Transcend.PL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarriersController : ControllerBase
{
    private readonly ICarrierService carrierService;

    public CarriersController(ICarrierService carrierService)
    {
        this.carrierService = carrierService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CarrierVM>>> GetAllCarriersAsync()
    {
        return await carrierService.GetAllCarriersAsync();
    }
}
