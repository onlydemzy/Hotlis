using KS.Domain.Common.Models;

namespace Hotlis.Domain.PaymentAggragate.ValueObjects;
public sealed class PaymentId : AggregateRootId<string>
{
    public override string Value{get;protected set;}
    private PaymentId(string value)
    {
        Value=value;
    }
    /*public static PaymentId CreateUnique()
    {
        return new PaymentId(Guid.NewGuid());
    }*/
    public static PaymentId Create(string value)
    {
        return new PaymentId(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}