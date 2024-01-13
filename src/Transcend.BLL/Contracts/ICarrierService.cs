using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transcend.Common.Models.Carrier;

namespace Transcend.BLL.Contracts;

public interface ICarrierService
{
    Task<List<CarrierVM>> GetAllCarriersAsync();
}
