using KS.Domain.Common.Models;

namespace Hotlis.Domain.RoomCategoryAggragate.ValueObjects;
public sealed class RoomCategoryId : AggregateRootId<string>
{
    public override string Value{get;protected set;}
    private RoomCategoryId(string value)
    {
        Value=value;
    }
       public static RoomCategoryId Create(string value)
    {
        return new RoomCategoryId(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}