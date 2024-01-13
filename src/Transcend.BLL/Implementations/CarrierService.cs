using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transcend.BLL.Contracts;
using Transcend.Common.Models.Carrier;
using Transcend.DAL.Models;

namespace Transcend.BLL.Implementations;

internal class CarrierService : ICarrierService
{
    private readonly UserManager<User> userManager;
    private readonly IMapper mapper;

    public CarrierService(UserManager<User> userManager, IMapper mapper)
    {
        this.userManager = userManager;
        this.mapper = mapper;
    }

    public async Task<List<CarrierVM>> GetAllCarriersAsync()
    {
        return this.mapper.Map<CarrierVM[]>(await this.userManager.GetUsersInRoleAsync("Carrier")).ToList();
    }
}
