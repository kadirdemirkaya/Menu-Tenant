using Microsoft.EntityFrameworkCore;
using Shared.Domain.BaseTypes;

namespace Shared.Domain.Aggregates.MenuAggregate.ValueObjects
{
    [Owned]
    public sealed class ProductDetail : ValueObject
    {
        public string? Description { get; private set; } // content
        public double? WeightInGrams { get; private set; } // gr bv.


        public ProductDetail(string description, double weightInGrams)
        {
            Description = description;
            WeightInGrams = weightInGrams;
        }

        public static ProductDetail Create(string description, double weightInGrams)
        {
            return new(description, weightInGrams);
        }

        public void ProductDetailSet(string description, double weightInGrams)
        {
            Description = description;
            WeightInGrams = weightInGrams;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Description;
            yield return WeightInGrams;
        }
    }
}
