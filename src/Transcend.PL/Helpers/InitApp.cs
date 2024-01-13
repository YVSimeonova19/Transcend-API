using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using Transcend.BLL.Contracts;
using Transcend.Common.Models.Carrier;

namespace Transcend.PL.Helpers;

public static class InitApp
{
    public static async Task InitAppAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
        var carrierService = scope.ServiceProvider.GetRequiredService<ICarrierService>();

        if ((await carrierService.GetAllCarriersAsync()).Count() != 0)
            return;

        var carriers = new List<CarrierIM> { 
            new CarrierIM {
                Username = "speedy",
                Password = "SpeedyP@55",
                Name = "Speedy"
            },
            new CarrierIM
            {
                Username = "econt",
                Password = "EcontP@55",
                Name = "Econt"
            },
            new CarrierIM
            {
                Username = "dhl",
                Password = "DhlP@55",
                Name = "DHL"
            }
        };

        foreach(var carrier in carriers)
        {
            await authService.CreateCarrierAsync(carrier);
        }
    }
}
