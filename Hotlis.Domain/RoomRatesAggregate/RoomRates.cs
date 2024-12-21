using Hotlis.Domain.Common.ValueObjects;
using Hotlis.Domain.RoomCategoryAggragate.ValueObjects;
using Hotlis.Domain.RoomRatesAggragate.ValueObjects;
using KS.Domain.Common.Models;

namespace Hotlis.Domain.RoomRatesAggragate;
public sealed class RoomRates:AggregateRoot<RoomRatesId,string>
{
    public string Plan{get;private set;}
    public byte Occupants{get;private set;}
    public Price Price{get;private set;}
    public RoomCategoryId RoomCategoryId{get;private set;}
    private RoomRates(RoomRatesId roomRatesId, string plan, RoomCategoryId roomCategoryId,byte occupants,Price price):base(roomRatesId)
    {
        Plan=plan;
        Price=price;
        RoomCategoryId=roomCategoryId;
        Occupants=occupants;
    }
    #pragma warning disable
    private RoomRates():base(default){}
    #pragma warning restore
    public static RoomRates Create(string roomRatesId,string plan,RoomCategoryId roomCategoryId,byte occupants,Price price)
    =>new(RoomRatesId.Create(roomRatesId),plan,roomCategoryId,occupants,price);
    
}