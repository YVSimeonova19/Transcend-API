using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transcend.Common.Models.Order;

public class OrderVM
{
    public string Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime ExpectedDeliveryDate { get; set; }
}
