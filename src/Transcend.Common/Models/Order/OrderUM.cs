using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transcend.Common.Utilities;

namespace Transcend.Common.Models.Order;

public class OrderUM
{
    public OrderStatus Status { get; set; }

    public DateTime ExpectedDeliveryDate { get; set; }
}
