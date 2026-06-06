using Valsy.Domain.Orders;
using Valsy.Domain.Products;

namespace Valsy.Application.Requests
{
    public record UpsertProductRequest
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }

        public List<ProductVariant> Variants { get; private set; } = new();
        public List<OrderItem> OrderItems { get; private set; } = new();
    }
}
