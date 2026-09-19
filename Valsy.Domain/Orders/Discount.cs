using Valsy.Domain.Common;
using Valsy.Domain.Common.Enums;

namespace Valsy.Domain.Discounts;

public class Discount : AggregateRoot<int>
{
    public string PromoCode { get; private set; } = string.Empty;
    public DiscountType Type { get; private set; }
    public decimal Value { get; private set; }
    public decimal? MinimumOrderAmount { get; private set; }
    public decimal? MaximumDiscountAmount { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public int? UsageLimit { get; private set; }
    public int UsageCount { get; private set; }
    public bool IsActive { get; private set; }

    private Discount() { }

    private Discount(string promoCode, DiscountType type, decimal value, decimal? minimumOrderAmount, decimal? maximumDiscountAmount, DateTime startDate, DateTime? endDate, int? usageLimit)
    {
        PromoCode = promoCode.Trim().ToUpperInvariant();
        Type = type;
        Value = value;
        MinimumOrderAmount = minimumOrderAmount;
        MaximumDiscountAmount = maximumDiscountAmount;
        StartDate = startDate;
        EndDate = endDate;
        UsageLimit = usageLimit;
        IsActive = true;
    }

    public static Discount Create(string promoCode, DiscountType type, decimal value, decimal? minimumOrderAmount, decimal? maximumDiscountAmount, DateTime startDate, DateTime? endDate, int? usageLimit)
    {
        if (string.IsNullOrWhiteSpace(promoCode))
            throw new ArgumentException("Promo code is required.", nameof(promoCode));

        if (value <= 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Discount value must be greater than zero.");

        if (type == DiscountType.Percentage && value > 100)
            throw new ArgumentOutOfRangeException(nameof(value), "Percentage discount cannot be greater than 100.");

        if (minimumOrderAmount < 0)
            throw new ArgumentOutOfRangeException(nameof(minimumOrderAmount));

        if (maximumDiscountAmount < 0)
            throw new ArgumentOutOfRangeException(nameof(maximumDiscountAmount));

        if (endDate.HasValue && endDate.Value < startDate)
            throw new ArgumentException("End date cannot be earlier than start date.", nameof(endDate));

        if (usageLimit.HasValue && usageLimit.Value <= 0)
            throw new ArgumentOutOfRangeException(nameof(usageLimit), "Usage limit must be greater than zero.");

        return new Discount(promoCode, type, value, minimumOrderAmount, maximumDiscountAmount, startDate, endDate, usageLimit);
    }

    public bool IsValid(DateTime now, decimal orderAmount)
    {
        if (!IsActive)
            return false;

        if (now < StartDate)
            return false;

        if (EndDate.HasValue && now > EndDate.Value)
            return false;

        if (MinimumOrderAmount.HasValue && orderAmount < MinimumOrderAmount.Value)
            return false;

        if (UsageLimit.HasValue && UsageCount >= UsageLimit.Value)
            return false;

        return true;
    }

    public decimal CalculateDiscount(decimal orderAmount)
    {
        if (orderAmount <= 0)
            return 0;

        var discountAmount = Type switch
        {
            DiscountType.Percentage => orderAmount * Value / 100,
            DiscountType.FixedAmount => Value,
            _ => throw new InvalidOperationException("Unsupported discount type.")
        };

        if (MaximumDiscountAmount.HasValue)
            discountAmount = Math.Min(discountAmount, MaximumDiscountAmount.Value);

        return Math.Min(discountAmount, orderAmount);
    }

    public void IncrementUsage()
    {
        if (UsageLimit.HasValue && UsageCount >= UsageLimit.Value)
            throw new InvalidOperationException("Discount usage limit has been reached.");

        UsageCount++;
    }

    public void Deactivate()
        => IsActive = false;


    public void Activate()
        => IsActive = true;

}