using Application.Common.Mapping;
using AutoMapper;
using Domain;

namespace Application.Orders.Queries.GetOrder
{
    public class OrderVm : IMapWith<Order>
    {
        public IEnumerable<Product> Products { get; set; }
        public Client Client { get; set; }
        public string Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderVm>()
                .ForMember(orderVm => orderVm.Products, options => options.MapFrom(order => order.Products))
                .ForMember(orderVm => orderVm.Client, options => options.MapFrom(order => order.Client))
                .ForMember(orderVm => orderVm.Status, options => options.MapFrom(order => order.Status));
        }
    }
}
