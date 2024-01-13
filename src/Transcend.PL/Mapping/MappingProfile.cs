using AutoMapper;
using Transcend.Common.Models.Carrier;
using Transcend.Common.Models.Order;
using Transcend.Common.Models.User;
using Transcend.DAL.Models;

namespace Transcend.PL.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        this.CreateMap<User, UserVM>()
            .ForMember(d => d.FirstName, c => c.MapFrom(s => s.UserDetails.FirstName))
            .ForMember(d => d.LastName, c => c.MapFrom(s => s.UserDetails.LastName))
            .ForMember(d => d.ShippingAddress, c => c.MapFrom(s => s.UserDetails.ShippingAddress));

        this.CreateMap<User, CarrierVM>()
            .ForMember(d => d.Name, c => c.MapFrom(s => s.Carrier.Name));

        this.CreateMap<OrderIM, Order>();
    }
}
