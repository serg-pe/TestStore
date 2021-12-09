using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Unit>
    {
        private readonly IStoreDbContext _dbContext;

        public CreateOrderCommandHandler(IStoreDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            foreach (var product in request.Products)
                product.Id = Guid.NewGuid();

            var order = new Order
            {
                Id = Guid.NewGuid(),
                Products = request.Products,
                Client = request.Client,
                Status = request.Status,
            };

            await _dbContext.Orders.AddAsync(order, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
