using Hotlis.Domain.Entities.ValueObjects;

namespace KS.Domain.Common.Models;
public abstract class AggregateRoot<TId, TIdType>:Entity<TId>
where TId:AggregateRootId<TIdType>
{
    public new AggregateRootId<TIdType> Id{get;protected set;}

    public TenantId TenantId{get;protected set;}
    protected AggregateRoot(TId id, TenantId tenantId)
    {
          Id=id;
          TenantId=tenantId?? throw new ArgumentNullException(nameof(tenantId));
    }
    #pragma  warning disable
    protected AggregateRoot()
    {

    }
    #pragma warning restore
}
 