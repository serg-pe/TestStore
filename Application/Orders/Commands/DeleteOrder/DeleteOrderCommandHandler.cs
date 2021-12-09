using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
    {
        private readonly IStoreDbContext _dbContext;

        public DeleteOrderCommandHandler(IStoreDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var deletedOrder = await _dbContext.Orders.Where(order => order.Id == request.OrderId).FirstOrDefaultAsync(cancellationToken);
            _dbContext.Orders.Remove(deletedOrder);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
