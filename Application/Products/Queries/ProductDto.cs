using Application.Common.Mapping;
using AutoMapper;
using Domain;

namespace Application.Products.Queries
{
    public class ProductDto : IMapWith<Product>
    {
        public string Id { get; set; }
        public string CategoryId { get; set; }
        public string? Img { get; set; }
        public string Description { get; set; }
        public int Pieces { get; set; }
        public decimal Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDto>()
                .ForMember(productDto => productDto.Id, options => options.MapFrom(product => product.Id.ToString()))
                .ForMember(productDto => productDto.CategoryId, options => options.MapFrom(product => product.Category.Id.ToString()))
                .ForMember(productDto => productDto.Img, options => options.MapFrom(product => product.Img))
                .ForMember(productDto => productDto.Description, options => options.MapFrom(product => product.Description))
                .ForMember(productDto => productDto.Pieces, options => options.MapFrom(product => product.Pieces))
                .ForMember(productDto => productDto.Price, options => options.MapFrom(product => product.Price));
        }
    }
}
