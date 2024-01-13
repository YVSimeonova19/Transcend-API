using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transcend.Common.Models.Order;

namespace Transcend.BLL.Contracts;

public interface IOrderService
{
    Task CreateOrderAsync(OrderIM orderIM, string userId);

    Task<List<OrderVM>> GetAllOrdersByIdAsync(string userId);

    Task<OrderVM> GetOrderInfoByIdAsync(int orderId);

    Task<List<OrderVM>> GetAllOrdersByIdCarrierAsync(string carrierId);

    Task<bool> CheckIfOrderExistsByIdAsync(int id);

    Task<OrderVM> UpdateOrderAsync(int id, OrderUM orderUM);
}
