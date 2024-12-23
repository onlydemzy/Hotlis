using Hotlis.Domain.Common.ValueObjects;
using Hotlis.Domain.Entities.ValueObjects;
using Hotlis.Domain.ServiceAggragate.ValueObjects;
using Hotlis.Domain.ServiceCategoryAggragate.ValueObjects;
using KS.Domain.Common.Models;

namespace Hotlis.Domain.ServiceAggregate;

public class Service:AggregateRoot<ServiceId, string>
{
    public string Name{get;private set;}
    public string Description{get;private set;}
    public bool IsTaxed{get;private set;}
    public decimal TaxRate{get;private set;}
    public Money Price{get;private set;}
    public ServiceCategoryId ServiceCategoryId  {get;private set;} 

    #pragma warning disable
    private Service():base(default,default){}
    #pragma warning restore

    private Service(ServiceId serviceId, string name, 
    string description, Money price,
    ServiceCategoryId serviceCategoryId, TenantId tenantId,decimal taxRate=0,bool isTaxed=false):base(serviceId,tenantId)
    {
        Name=name;
        Description=description;
        Price=price;
        ServiceCategoryId=serviceCategoryId;
        TaxRate=taxRate;
        IsTaxed=isTaxed;

    }

    public static Service Create(string serviceId, string name, string description, 
    Money price,ServiceCategoryId serviceCategoryId, TenantId tenantId, decimal taxRate=0, bool isTaxed=false)
    {
        string id;
        if(serviceId is null)
        {
            id=Guid.NewGuid().ToString();
        }
        else
        {
            id=serviceId;
        }
        return new(ServiceId.Create(id), name, description, price, serviceCategoryId, tenantId, taxRate, isTaxed);
    }

}
