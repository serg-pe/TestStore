using Application.Common.Mapping;
using Application.Orders.Commands.DeleteOrder;
using AutoMapper;

namespace StoreWebApi.Models
{
    public class DeleteOrderDto : IMapWith<DeleteOrderCommand>
    {
        public string OrderId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteOrderDto, DeleteOrderCommand>()
                .ForMember(deleteOrderCommand => deleteOrderCommand.OrderId, options => options.MapFrom(deleteOrderDto => Guid.Parse(deleteOrderDto.OrderId)));
        }
    }
}
