using Application.Common.Mapping;
using Application.Products.Commands.UpdateProduct;
using AutoMapper;

namespace StoreWebApi.Models
{
    public class UpdateProductDto : IMapWith<UpdateProductCommand>
    {
        public string CategoryId { get; set; }
        public string? Img { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Pieces { get; set; }
        public decimal Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProductDto, UpdateProductCommand>()
                .ForMember(updateProductCommand => updateProductCommand.CategoryId, options => options.MapFrom(updateProductDto => updateProductDto.CategoryId))
                .ForMember(updateProductCommand => updateProductCommand.Img, options => options.MapFrom(updateProductDto => updateProductDto.Img))
                .ForMember(updateProductCommand => updateProductCommand.Name, options => options.MapFrom(updateProductDto => updateProductDto.Name))
                .ForMember(updateProductCommand => updateProductCommand.Description, options => options.MapFrom(updateProductDto => updateProductDto.Description))
                .ForMember(updateProductCommand => updateProductCommand.Pieces, options => options.MapFrom(updateProductDto => updateProductDto.Pieces))
                .ForMember(updateProductCommand => updateProductCommand.Price, options => options.MapFrom(updateProductDto => updateProductDto.Price));
        }
    }
}
