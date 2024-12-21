using KS.Domain.Common.Models;

namespace Hotlis.Domain.Entities.ValueObjects;
public sealed class TenantId : AggregateRootId<Guid>
{
    public override Guid Value{get;protected set;}
    private TenantId(Guid value)
    {
        Value=value;
    }
    public static TenantId CreateUnique()
    {
        return new TenantId(Guid.NewGuid());
    }
    public static TenantId Create(Guid value)
    {
        return new TenantId(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}