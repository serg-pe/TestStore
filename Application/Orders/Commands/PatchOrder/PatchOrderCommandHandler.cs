using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.PatchOrder
{
    public class PatchOrderCommandHandler : IRequestHandler<PatchOrderCommand, Unit>
    {
        private readonly IStoreDbContext _dbContext;

        public PatchOrderCommandHandler(IStoreDbContext dbContext) => 
            _dbContext = dbContext;

        public async Task<Unit> Handle(PatchOrderCommand request, CancellationToken cancellationToken)
        {
            var patchedOrder = await _dbContext.Orders.FirstAsync(order => order.Id == request.OrderId);
            request.OrderPatch.ApplyTo(patchedOrder);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
