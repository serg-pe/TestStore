using Application.Common.Mapping;
using AutoMapper;
using Domain;

namespace Application.Categories.Queries.GetCategoriesList
{
    public class CategoryDto : IMapWith<Category>
    {
        public string Id { get; set; }
        public string? Img { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryDto>()
                .ForMember(categoryDto => categoryDto.Id, options => options.MapFrom(category => category.Id))
                .ForMember(categoryDto => categoryDto.Img, options => options.MapFrom(category => category.Img))
                .ForMember(categoryDto => categoryDto.Name, options => options.MapFrom(category => category.Name))
                .ForMember(categoryDto => categoryDto.Description, options => options.MapFrom(category => category.Description));
        }
    }
}
