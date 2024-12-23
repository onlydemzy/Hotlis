using Hotlis.Domain.Common.ValueObjects;
using Hotlis.Domain.Entities.ValueObjects;
using Hotlis.Domain.RoomCategoryAggragate.ValueObjects;
using Hotlis.Domain.RoomRatesAggragate.ValueObjects;
using KS.Domain.Common.Models;

namespace Hotlis.Domain.RoomRatesAggragate;
public sealed class RoomRates:AggregateRoot<RoomRatesId,string>
{
    public string Plan{get;private set;}
    public byte Occupants{get;private set;}
    public Money Money{get;private set;}
    public RoomCategoryId RoomCategoryId{get;private set;}
    private RoomRates(RoomRatesId roomRatesId, string plan, RoomCategoryId roomCategoryId,
    byte occupants,Money price, TenantId tenantId):base(roomRatesId,tenantId)
    {
        Plan=plan;
        Money=price;
        RoomCategoryId=roomCategoryId;
        Occupants=occupants;
    }
    
    #pragma warning disable
    private RoomRates():base(default,default){}
    #pragma warning restore
    public static RoomRates Create(string roomRatesId,string plan,RoomCategoryId roomCategoryId,
    byte occupants,Money price,TenantId tenantId)
    {
        string id;
        if(roomRatesId is null)
        {
            id=Guid.NewGuid().ToString();
        }
        else
        {
            id=roomRatesId;
        }
        return new(RoomRatesId.Create(id),plan,roomCategoryId,occupants,price,tenantId);
    }
    
    
}