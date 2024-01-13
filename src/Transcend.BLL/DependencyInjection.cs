using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transcend.BLL.Contracts;
using Transcend.BLL.Implementations;

namespace Transcend.BLL;

public static class DependencyInjection
{
    public static void AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<ICurrentUser, CurrentUser>()
            .AddScoped<ICarrierService, CarrierService>();
    }
}
