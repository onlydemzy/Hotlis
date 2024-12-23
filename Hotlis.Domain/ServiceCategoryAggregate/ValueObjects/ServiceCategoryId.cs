using KS.Domain.Common.Models;

namespace Hotlis.Domain.ServiceCategoryAggragate.ValueObjects;
public sealed class ServiceCategoryId : AggregateRootId<string>
{
    public override string Value{get;protected set;}
    private ServiceCategoryId(string value)
    {
        Value=value;
    }
    public static ServiceCategoryId CreateUnique()
    {
        return new ServiceCategoryId(Guid.NewGuid().ToString());
    }
    public static ServiceCategoryId Create(string value)
    {
        return new ServiceCategoryId(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}