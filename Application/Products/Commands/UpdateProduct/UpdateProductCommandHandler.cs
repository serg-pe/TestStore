using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IStoreDbContext _dbContext;

        public UpdateProductCommandHandler(IStoreDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.SingleAsync(product => product.Id == request.ProductId);
            product.Category = await _dbContext.Categories.SingleAsync(category => category.Id == Guid.Parse(request.CategoryId), cancellationToken);
            product.Img = request.Img;
            product.Name = request.Name;
            product.Description = request.Description;
            product.Pieces = request.Pieces;
            product.Price = request.Price;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
