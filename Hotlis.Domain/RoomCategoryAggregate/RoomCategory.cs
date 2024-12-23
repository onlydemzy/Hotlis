using Hotlis.Domain.Entities.ValueObjects;
using Hotlis.Domain.RoomCategoryAggragate.ValueObjects;
using Hotlis.Domain.RoomRatesAggragate.ValueObjects;
using KS.Domain.Common.Models;

namespace Hotlis.Domain.RoomCategoryAggragate;

public sealed class RoomCategory:AggregateRoot<RoomCategoryId,string>
{
    
    private readonly List<RoomRatesId> _roomRateIds=[];
    public string Title{get;private set;}
    public string Description{get;private set;}
    public IReadOnlyList<RoomRatesId> RoomRateIds=> _roomRateIds.AsReadOnly();

    #pragma warning disable
    private RoomCategory():base(default,default){}
    #pragma  warning restore

    private RoomCategory(RoomCategoryId roomCategoryId, string title, string description, TenantId tenantId)
    :base(roomCategoryId,tenantId)
    {
        Title = title;
        Description = description;
       
    }
    public static RoomCategory Create(string roomCategoryId,string title,string description, TenantId tenantId)
    =>new(RoomCategoryId.Create(roomCategoryId),title,description, tenantId);

    

}