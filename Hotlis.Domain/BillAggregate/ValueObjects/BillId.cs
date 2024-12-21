using KS.Domain.Common.Models;

namespace Hotlis.Domain.BillAggragate.ValueObjects;
public sealed class BillId : AggregateRootId<string>
{
    public override string Value{get;protected set;}
    private BillId(string value)
    {
        Value=value;
    }
    /*public static BillId CreateUnique()
    {
        return new BillId(Guid.NewGuid());
    }*/
    public static BillId Create(string value)
    {
        return new BillId(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}