using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Unit>
    {
        private readonly IStoreDbContext _dbContext;

        public CreateProductCommandHandler(IStoreDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await _dbContext.Products.AddAsync(new Product
            {
                Id = Guid.NewGuid(),
                Category = await _dbContext.Categories.FirstAsync(category => category.Id == Guid.Parse(request.CategoryId)),
                Img = request.Img,
                Name = request.Name,
                Description = request.Description,
                Pieces = request.Pieces,
                Price = request.Price,
            });
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
