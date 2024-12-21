using KS.Domain.Common.Models;

namespace Hotlis.Domain.RoomRatesAggragate.ValueObjects;
public sealed class RoomRatesId : AggregateRootId<string>
{
    public override string Value{get;protected set;}
    private RoomRatesId(string value)
    {
        Value=value;
    }
    
    public static RoomRatesId Create(string value)
    {
        return new RoomRatesId(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}