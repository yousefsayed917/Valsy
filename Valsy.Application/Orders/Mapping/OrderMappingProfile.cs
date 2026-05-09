using AutoMapper;
using Valsy.Application.Orders.Dtos;
using Valsy.Domain;

namespace Valsy.Application.Orders.Mapping;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<Order, OrderDto>();
    }
}
