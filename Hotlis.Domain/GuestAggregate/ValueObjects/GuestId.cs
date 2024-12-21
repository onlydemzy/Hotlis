using KS.Domain.Common.Models;

namespace Hotlis.Domain.GuestAggragate.ValueObjects;
public sealed class GuestId : AggregateRootId<Guid>
{
    public override Guid Value{get;protected set;}
    private GuestId(Guid value)
    {
        Value=value;
    }
    public static GuestId CreateUnique()
    {
        return new GuestId(Guid.NewGuid());
    }
    public static GuestId Create(Guid value)
    {
        return new GuestId(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}