namespace Valsy.Domain
{
    public class ProductVariant
    {
        public Guid Id { get; set; }

        public string Size { get; set; } // S, M, L

        public string Color { get; set; }

        public int Stock { get; set; }

        public Guid ProductId { get; set; }
    }
}
