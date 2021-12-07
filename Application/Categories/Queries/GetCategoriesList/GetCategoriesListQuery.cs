using MediatR;

namespace Application.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery : IRequest<CategoriesListVm>
    { }
}
