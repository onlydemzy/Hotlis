using Hotlis.Domain.Common.ValueObjects;
using Hotlis.Domain.ConsumedServiceAggragate.ValueObjects;
using Hotlis.Domain.Entities.ValueObjects;
using Hotlis.Domain.ServiceAggregate;
using KS.Domain.Common.Models;

namespace Hotlis.Domain.ConsumedServiceAggregate;

public class ConsumedServiceDetail:AggregateRoot<ConsumedServiceDetailId, long>
{
    public byte Qty{get;private set;}
    public Service Service{get;private set;}//navigation property
    public Money Price{get;private set;}
    public decimal Tax{get;private set;}
    public decimal Discount{get;private set;}
    //public ConsumedServiceId ConsumedServiceId{get;private set;}
    public Money Amount=>new (Qty*Price.Amount,Price.Currency);


    #pragma warning disable
    private ConsumedServiceDetail():base(default,default){}
    #pragma warning restore

    private ConsumedServiceDetail(ConsumedServiceDetailId ConsumedServiceDetailId,Service service, byte qty,Money price,
       decimal tax,TenantId tenantId):base(ConsumedServiceDetailId,tenantId)
    {
        Service=service;
        Qty=qty;
        Price=price;
        Tax=tax;
        
    }

    public static ConsumedServiceDetail Create(long consumedServiceDetailId,Service service,byte qty,Money price,
    TenantId tenantId,decimal tax=0)
    {
        
        return new(ConsumedServiceDetailId.Create(consumedServiceDetailId), service, qty,price,tax,tenantId);
    }

}
