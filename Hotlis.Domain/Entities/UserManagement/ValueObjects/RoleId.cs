using KS.Domain.Common.Models;

namespace KS.Domain.UserManagement.ValueObjects;
public sealed class RoleId : ValueObject
{
    public Guid Value{get;private set;}
    private RoleId(Guid value)
    {
        Value=value;
    }
    public static RoleId CreateUnique()
    {
        return new RoleId(Guid.NewGuid());
    }
    public static RoleId Create(Guid value)
    {
        return new RoleId(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}