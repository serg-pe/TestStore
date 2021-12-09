using Application.Common.Mapping;
using Application.Orders.Commands.CreateOrder;
using AutoMapper;
using Domain;

namespace StoreWebApi.Models
{
    public class CreateOrderDto : IMapWith<CreateOrderCommand>
    {
        public IEnumerable<Product> Products { get; set; }
        public Client Client { get; set; }
        public string Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateOrderDto, CreateOrderCommand>()
                .ForMember(createOrderCommand => createOrderCommand.Products, options => options.MapFrom(createOrderCommand => createOrderCommand.Products))
                .ForMember(createOrderCommand => createOrderCommand.Client, options => options.MapFrom(createOrderCommand => createOrderCommand.Client))
                .ForMember(createOrderCommand => createOrderCommand.Status, options => options.MapFrom(createOrderCommand => createOrderCommand.Status));
        }
    }
}
