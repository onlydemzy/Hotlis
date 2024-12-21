using KS.Domain.Common.Models;

namespace KS.Domain.UserManagement.ValueObjects;
public sealed class UserId : ValueObject
{
    public Guid Value{get;private set;}
    private UserId(Guid value)
    {
        Value=value;
    }
    public static UserId CreateUnique()
    {
        return new UserId(Guid.NewGuid());
    }
    public static UserId Create(string value)
    {
        return new UserId(Guid.Parse(value));
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}