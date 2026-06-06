using MediatR;
using Valsy.Application.Requests;

namespace Valsy.Application.Products.Commands.CreateProduct;

public record CreateProductCommand : IRequest<int>
{
    public UpsertProductRequest productRequest { get; set; }
}

