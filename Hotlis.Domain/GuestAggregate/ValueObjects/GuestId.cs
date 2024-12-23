using KS.Domain.Common.Models;

namespace Hotlis.Domain.GuestAggragate.ValueObjects;
public sealed class GuestId : AggregateRootId<string>
{
    public override string Value{get;protected set;}
    private GuestId(string value)
    {
        Value=value;
    }
    /*public static GuestId CreateUnique()
    {
        return new GuestId(Guid.NewGuid());
    }*/
    public static GuestId Create(string value)
    {
        return new GuestId(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}