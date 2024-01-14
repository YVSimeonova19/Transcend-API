using Transcend.Common.Utilities;

namespace Transcend.Common.Models.Order;

public class OrderUM
{
    public OrderStatus Status { get; set; }

    public DateTime ExpectedDeliveryDate { get; set; }
}
