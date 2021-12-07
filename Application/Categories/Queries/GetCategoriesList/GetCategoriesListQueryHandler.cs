using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, CategoriesListVm>
    {
        private readonly IMapper _mapper;
        private readonly IStoreDbContext _dbContext;

        public GetCategoriesListQueryHandler(IMapper mapper, IStoreDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<CategoriesListVm> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var categories = await _mapper.ProjectTo<CategoryDto>(_dbContext.Categories).ToListAsync(cancellationToken);
            return new CategoriesListVm { Categories = categories };
        }
    }
}
