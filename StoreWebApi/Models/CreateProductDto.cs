using Application.Common.Mapping;
using Application.Products.Commands.CreateProduct;
using AutoMapper;

namespace StoreWebApi.Models
{
    public class CreateProductDto : IMapWith<CreateProductCommand>
    {
        public string CategoryId { get; set; }
        public string? Img { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Pieces { get; set; }
        public decimal Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProductDto, CreateProductCommand>()
                .ForMember(createProductComand => createProductComand.CategoryId, options => options.MapFrom(createProductDto => createProductDto.CategoryId))
                .ForMember(createProductComand => createProductComand.Img, options => options.MapFrom(createProductDto => createProductDto.Img))
                .ForMember(createProductComand => createProductComand.Name, options => options.MapFrom(createProductDto => createProductDto.Name))
                .ForMember(createProductComand => createProductComand.Description, options => options.MapFrom(createProductDto => createProductDto.Description))
                .ForMember(createProductComand => createProductComand.Pieces, options => options.MapFrom(createProductDto => createProductDto.Pieces))
                .ForMember(createProductComand => createProductComand.Price, options => options.MapFrom(createProductDto => createProductDto.Price));
        }
    }
}
