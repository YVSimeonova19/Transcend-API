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
}
