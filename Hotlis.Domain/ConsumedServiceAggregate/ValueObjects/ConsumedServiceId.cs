using KS.Domain.Common.Models;

namespace Hotlis.Domain.ServiceConsumedAggragate.ValueObjects;
public sealed class ConsumedServiceId : AggregateRootId<string>
{
    public override string Value{get;protected set;}
    private ConsumedServiceId(string value)
    {
        Value=value;
    }
    public static ConsumedServiceId CreateUnique()
    {
        return new ConsumedServiceId(Guid.NewGuid().ToString());
    }
    public static ConsumedServiceId Create(string value)
    {
        return new ConsumedServiceId(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}