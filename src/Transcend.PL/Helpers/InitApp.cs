using Transcend.BLL.Contracts;
using Transcend.Common.Models.Carrier;

namespace Transcend.PL.Helpers;

public static class InitApp
{
    // Initialize the app and create carrier users if there are no users in the DB
    public static async Task InitAppAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
        var carrierService = scope.ServiceProvider.GetRequiredService<ICarrierService>();

        // Check if there are users in the DB
        if ((await carrierService.GetAllCarriersAsync()).Count() != 0)
            return;

        // Create carrier users
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

        // Add the carrier users to the DB
        foreach(var carrier in carriers)
        {
            await authService.CreateCarrierAsync(carrier);
        }
    }
}
