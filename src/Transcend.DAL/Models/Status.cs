using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transcend.DAL.Models;

public enum Status
{
    WaitingForConfirmation,
    Denied,
    Accepted,
    RecievedByCarrier,
    AwaitingDelivery,
    Delivered
}
