using Application.Common.Mapping;
using AutoMapper;
using Domain;

namespace Application.Orders.Queries.GetOrdersList
{
    public class OrderDto : IMapWith<Order>
    {
        public string Id { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public Client Client { get; set; }
        public string Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDto>()
                .ForMember(orderDto => orderDto.Id, options => options.MapFrom(order => order.Id.ToString()))
                .ForMember(orderDto => orderDto.Products, options => options.MapFrom(order => order.Products))
                .ForMember(orderDto => orderDto.Client, options => options.MapFrom(order => order.Client))
                .ForMember(orderDto => orderDto.Status, options => options.MapFrom(order => order.Status));
        }
    }
}
