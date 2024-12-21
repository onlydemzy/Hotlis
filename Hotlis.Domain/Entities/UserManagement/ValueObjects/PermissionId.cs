using KS.Domain.Common.Models;

namespace KS.Domain.UserManagement.ValueObjects;
public sealed class PermissionId : ValueObject
{
    public Guid Value{get;private set;}
    private PermissionId(Guid value)
    {
        Value=value;
    }
    public static PermissionId CreateUnique()
    {
        return new PermissionId(Guid.NewGuid());
    }
    public static PermissionId Create(Guid value)
    {
        return new PermissionId(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}