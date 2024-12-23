using Hotlis.Domain.Common.ValueObjects;
using Hotlis.Domain.Entities.ValueObjects;
using Hotlis.Domain.ConsumedServiceAggragate.ValueObjects;

using KS.Domain.Common.Models;
using Hotlis.Domain.ServiceAggragate.ValueObjects;
using Hotlis.Domain.ServiceConsumedAggragate.ValueObjects;
using Hotlis.Domain.BillAggragate;

namespace Hotlis.Domain.ConsumedServiceAggregate;

public class ConsumedService:AggregateRoot<ConsumedServiceId, string>
{
    private readonly List<ConsumedServiceDetail> _consumedServiceDetails=[];
    public string Description{get;private set;}
    public string Employee{get;private set;}
    public DateTime ServiceDate{get;private set;}
    public bool Billed {get;private set;}
    //public Money Amount=>new(_consumedServiceDetails.Sum(a=>a.Amount.Amount),_consumedServiceDetails.First().Amount.Currency);

    public IReadOnlyList<ConsumedServiceDetail> ConsumedServiceDetails=>_consumedServiceDetails.AsReadOnly();
    

    #pragma warning disable
    private ConsumedService():base(default,default){}
    #pragma warning restore

    private ConsumedService(ConsumedServiceId consumedServiceId,string description, string employee, 
     bool billed,List<ConsumedServiceDetail>? consumedServiceDetails, TenantId tenantId):base(consumedServiceId,tenantId)
    {
        Description=description;
        Employee=employee;
        ServiceDate=DateTime.UtcNow;
        Billed=billed;
        if(consumedServiceDetails is not null)
        {
            _consumedServiceDetails=consumedServiceDetails;
        }
        

    }
    

    public static ConsumedService Create(string consumedServiceId, string description, string employee,
        bool billed, List<ConsumedServiceDetail>? consumedServiceDetails,  TenantId tenantId)
    {
        string id;
        if(consumedServiceId is null)
        {
            id=Guid.NewGuid().ToString();
        }
        else
        {
            id=consumedServiceId;
        }
        return new(ConsumedServiceId.Create(id), description, employee,billed, consumedServiceDetails??[], tenantId);
    }

    public static void BillService(ConsumedService service)
    {
        service.Billed=true;
    }

}
