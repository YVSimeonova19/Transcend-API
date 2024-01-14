using Transcend.Common.Models.Order;

namespace Transcend.BLL.Contracts;

public interface IOrderService
{
    // Add a new order to the DB asyncronously
    Task CreateOrderAsync(OrderIM orderIM, string userId);

    // Retrieve all orders thet belong to a certain customer from the DB asyncronously
    Task<List<OrderVM>> GetAllOrdersByIdAsync(string userId);

    // Get the information of an order by its id asyncronously
    Task<OrderVM> GetOrderInfoByIdAsync(int orderId);

    // Retrieve all orders thet belong to a certain carrier from the DB asyncronously
    Task<List<OrderVM>> GetAllOrdersByIdCarrierAsync(string carrierId);

    // Check if an order exists by its id asyncronously
    Task<bool> CheckIfOrderExistsByIdAsync(int id);

    // Update an orders information asyncronously
    Task<OrderVM> UpdateOrderAsync(int id, OrderUM orderUM);
}
