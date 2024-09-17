using AutoMapper;
using Domain.Entities;
using UseCases.Order.Dtos;

namespace UseCases.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Domain.Entities.Order, OrderDto>().
                ReverseMap();
            CreateMap<CreateOrderDto, Domain.Entities.Order>()
                .ReverseMap();
            CreateMap<OrderItemDto, OrderItem>()
                .ReverseMap();
        }
    }
}
