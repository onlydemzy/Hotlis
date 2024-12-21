using KS.Domain.Common.Models;

namespace Hotlis.Domain.RoomAggragate.ValueObjects;
public sealed class RoomId : AggregateRootId<string>
{
    public override string Value{get;protected set;}
    private RoomId(string value)
    {
        Value=value;
    }
    /*public static RoomId CreateUnique()
    {
        return new RoomId(Value);
    }*/
    public static RoomId Create(string value)
    {
        return new RoomId(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}