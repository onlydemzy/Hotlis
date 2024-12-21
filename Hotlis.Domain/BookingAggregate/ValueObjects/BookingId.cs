using KS.Domain.Common.Models;

namespace Hotlis.Domain.BookingAggragate.ValueObjects;
public sealed class BookingId : AggregateRootId<string>
{
    public override string Value{get;protected set;}
    private BookingId(string value)
    {
        Value=value;
    }
    
    public static BookingId Create(string value)
    {
        return new BookingId(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}