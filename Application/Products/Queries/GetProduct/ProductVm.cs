using Application.Common.Mapping;
using AutoMapper;
using Domain;

namespace Application.Products.Queries.GetProduct
{
    public class ProductVm : IMapWith<Product>
    {
        public string? Img { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Pieces { get; set; }
        public decimal Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductVm>()
                .ForMember(productVm => productVm.Img, options => options.MapFrom(product => product.Img))
                .ForMember(productVm => productVm.Name, options => options.MapFrom(product => product.Name))
                .ForMember(productVm => productVm.Description, options => options.MapFrom(product => product.Description))
                .ForMember(productVm => productVm.Pieces, options => options.MapFrom(product => product.Pieces))
                .ForMember(productVm => productVm.Price, options => options.MapFrom(product => product.Price));
        }
    }
}
