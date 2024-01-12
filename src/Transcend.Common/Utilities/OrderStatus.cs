using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transcend.Common.Utilities;

public enum OrderStatus
{
    WaitingForConfirmation,
    Denied,
    Accepted,
    RecievedByCarrier,
    AwaitingDelivery,
    Delivered
}
