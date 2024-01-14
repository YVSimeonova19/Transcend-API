using Transcend.Common.Models.Carrier;

namespace Transcend.BLL.Contracts;

public interface ICarrierService
{
    // Get a list of all carriers
    Task<List<CarrierVM>> GetAllCarriersAsync();
}
