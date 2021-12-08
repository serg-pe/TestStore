using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IStoreDbContext _dbContext;

        public DeleteProductCommandHandler(IStoreDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var deletingProduct = await _dbContext.Products.FirstAsync(product => product.Id == Guid.Parse(request.ProductId), cancellationToken);
            _dbContext.Products.Remove(deletingProduct);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
