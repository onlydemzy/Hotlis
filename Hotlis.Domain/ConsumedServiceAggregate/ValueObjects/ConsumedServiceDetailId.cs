using KS.Domain.Common.Models;

namespace Hotlis.Domain.ConsumedServiceAggragate.ValueObjects;
public sealed class ConsumedServiceDetailId : AggregateRootId<long>
{
    public override long Value { get; protected set; }
    private ConsumedServiceDetailId(long value)
    {
        Value = value;
    }
    /*public static ConsumedServiceDetailId CreateUnique()
    {
        return new ConsumedServiceDetailId(long);
    }*/
    public static ConsumedServiceDetailId Create(long value)
    {
        return new ConsumedServiceDetailId(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}