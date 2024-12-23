using Hotlis.Domain.Entities.ValueObjects;
using Hotlis.Domain.ServiceCategoryAggragate.ValueObjects;

using KS.Domain.Common.Models;

namespace Hotlis.Domain.ServiceCategoryAggregate;

public class ServiceCategory:AggregateRoot<ServiceCategoryId, string>
{
    public string Name{get;private set;}
    public string Description{get;private set;}
    //public IReadOnlyList<ServiceId> ServiceIds=>_serviceIds.AsReadOnly();
  
    #pragma warning disable
    private ServiceCategory():base(default,default){}
    #pragma warning restore

    private ServiceCategory(ServiceCategoryId serviceCategoryId, string name, string description,
     TenantId tenantId):base(serviceCategoryId,tenantId)
    {
        Name=name;
        Description=description;
       
    }

    public static ServiceCategory Create(string serviceCategoryId,string name, string description,TenantId tenantId)
    {
        string id;
        if (serviceCategoryId is null)
        {
            id = Guid.NewGuid().ToString();
        }
        else
        {
            id = serviceCategoryId;
        }
        return new(ServiceCategoryId.Create(id), name, description, tenantId);
    }

}
