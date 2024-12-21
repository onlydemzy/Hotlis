namespace KS.Domain.Common.Models;
public abstract class Entity<TId>:IEquatable<Entity<TId>>,IHasDomainEvents
where TId:ValueObject//notnull
{
    private readonly List<IDomainEvent> _domainEvents=[];
    public TId Id{get; private set;}
    public IReadOnlyList<IDomainEvent> DomainEvents=>_domainEvents.AsReadOnly();
    protected Entity(TId id)
    {
        Id=id;
    }
    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    #pragma warning disable
    protected Entity(){}
    #pragma warning restore
    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
    }

    public bool Equals(Entity<TId>? other)
    {
       return other is Entity<TId> entity && Id.Equals(entity.Id);
    }

    public static bool operator==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left,right);
    }
    public static bool operator!=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left,right);
    }

    public override int GetHashCode()
    {
        return (GetType()
        .ToString() +Id).GetHashCode();
    }
}