using KS.Domain.Common.Models;

namespace Hotlis.Domain.Common.ValueObjects;
public class Money(decimal amount, string currency) : ValueObject
{
    public decimal Amount { get; private set; } = amount;
    public string Currency { get; private set; } = currency;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}