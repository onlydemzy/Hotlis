using KS.Domain.Common.Models;

namespace Hotlis.Domain.ServiceAggragate.ValueObjects;
public sealed class ServiceId : AggregateRootId<string>
{
    public override string Value{get;protected set;}
    private ServiceId(string value)
    {
        Value=value;
    }
    /*public static ServiceId CreateUnique()
    {
        return new ServiceId(Guid.NewGuid());
    }*/
    public static ServiceId Create(string value)
    {
        return new ServiceId(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}