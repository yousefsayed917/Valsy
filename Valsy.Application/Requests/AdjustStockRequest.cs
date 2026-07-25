namespace Valsy.Application.Requests
{
    public record AdjustStockRequest(
    int ProductVariantId,
    int NewStock);
}
