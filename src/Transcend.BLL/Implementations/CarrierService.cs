using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transcend.BLL.Contracts;
using Transcend.Common.Models.Carrier;
using Transcend.DAL.Data;
using Transcend.DAL.Models;

namespace Transcend.BLL.Implementations;

internal class CarrierService : ICarrierService
{
    private readonly UserManager<User> userManager;
    private readonly TranscendDBContext dbContext;
    private readonly IMapper mapper;
    private readonly RoleManager<IdentityRole> roleManagaer;

    public CarrierService(UserManager<User> userManager, TranscendDBContext dbContext, IMapper mapper, RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.roleManagaer = roleManager;
    }

    public async Task<List<CarrierVM>> GetAllCarriersAsync()
    {
        return await this.userManager.Users.Include(u => u.Carrier).Where(u => u.Carrier != null).ProjectTo<CarrierVM>(this.mapper.ConfigurationProvider).ToListAsync();
    }
}
