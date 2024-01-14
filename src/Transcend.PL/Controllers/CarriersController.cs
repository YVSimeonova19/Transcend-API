using Microsoft.AspNetCore.Mvc;
using Transcend.BLL.Contracts;
using Transcend.Common.Models.Carrier;

namespace Transcend.PL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarriersController : ControllerBase
{
    private readonly ICarrierService carrierService;

    // Add dependency injections
    public CarriersController(ICarrierService carrierService)
    {
        this.carrierService = carrierService;
    }

    // Display all carriers in the DB
    [HttpGet]
    public async Task<ActionResult<List<CarrierVM>>> GetAllCarriersAsync()
    {
        return await carrierService.GetAllCarriersAsync();
    }
}
