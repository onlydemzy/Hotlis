using Hotlis.Domain.Entities.ValueObjects;
using Hotlis.Domain.RoomAggragate.ValueObjects;
using Hotlis.Domain.RoomCategoryAggragate.ValueObjects;
using KS.Domain.Common.Models;

namespace Hotlis.Domain.RoomAggragate;

public sealed class Room:AggregateRoot<RoomId,string>
{
    public string RoomNumber{get;private set;}
    public string Status{get;private set;}
    public RoomCategoryId RoomCategoryId{get;private set;}
    
    #pragma warning disable
    private Room():base(default,default){}
    #pragma warning restore

    private Room(RoomId roomId,string roomNumber, string status,RoomCategoryId roomCategoryId,TenantId tenantId)
    :base(roomId,tenantId)
    {
       Status=status;
       RoomCategoryId=roomCategoryId;
       RoomNumber=roomNumber;
    }
    public static Room Create(string roomId,string roomNumber,
    string status,RoomCategoryId roomCategoryId,TenantId tenantId)
    =>new(RoomId.Create(roomId),roomNumber,status,roomCategoryId,tenantId);

    public static void UpdateRoomStatus(Room room,string newStatus)=>room.Status=newStatus;

    public static void UpdateRoomCategory(Room room, RoomCategoryId roomCategoryId)
    =>room.RoomCategoryId=roomCategoryId;

}