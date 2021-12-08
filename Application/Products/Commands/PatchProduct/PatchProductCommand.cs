using Domain;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Products.Commands.PatchProduct
{
    public class PatchProductCommand : IRequest
    {
        public string ProductId { get; set; }
        public JsonPatchDocument<Product> ProductPatch { get; set; }
    }
}
