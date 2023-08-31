namespace Infrastructure.AutomapperProfile;
using AutoMapper;
using Domain.Dtos.Client;
using Domain.Dtos.Order;
using Domain.Entities;


public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<GetOrder, Order>();
        CreateMap<Order, GetOrder>();
        CreateMap<GetClient,Client>();
    }
}
