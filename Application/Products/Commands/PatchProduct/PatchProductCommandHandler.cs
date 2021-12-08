using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands.PatchProduct
{
    public class PatchProductCommandHandler : IRequestHandler<PatchProductCommand>
    {
        private readonly IStoreDbContext _dbContext;

        public PatchProductCommandHandler(IStoreDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(PatchProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.SingleAsync(product => product.Id == Guid.Parse(request.ProductId));
            request.ProductPatch.ApplyTo(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
