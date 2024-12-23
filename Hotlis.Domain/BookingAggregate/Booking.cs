using Hotlis.Domain.BookingAggragate.ValueObjects;
using Hotlis.Domain.Common.ValueObjects;
using Hotlis.Domain.Entities.ValueObjects;
using Hotlis.Domain.GuestAggragate.ValueObjects;
using Hotlis.Domain.RoomAggragate.ValueObjects;
using Hotlis.Domain.RoomCategoryAggragate.ValueObjects;
using KS.Domain.Common.Models;

namespace Hotlis.Domain.BookingAggragate;
public sealed class Booking:AggregateRoot<BookingId,string>
{
    public GuestId GuestId{get;private set;}
    public RoomCategoryId RoomCategoryId{get;private set;}  
    public DateTime BookDate{get;private set;}
    public DateTime CheckInDate{get;private set;}
    public DateTime CheckOutDate{get;private set;}
    public RoomId? RoomId{get;private set;}
    public Money BookRate{get;private set;}
    public decimal Discount{get;private set;}
    public bool PauseBooking{get;private set;}
    public string Segment{get;private set;}// walking, ota, travel agent
    public string BookedBy{get;private set;}
    public string Status{get;private set;}

    #pragma warning disable
    private Booking():base(default,default){}
    #pragma warning restore

    private Booking(BookingId bookingId,GuestId guestId,RoomCategoryId roomCategoryId,DateTime bookDate,DateTime checkInDate,
        DateTime checkoutDate,Money bookRate,decimal discount, string segment,string bookedBy,TenantId tenantId):base(bookingId,tenantId)
        {
            GuestId = guestId;
            BookDate = bookDate;
            CheckInDate = checkInDate;
            CheckOutDate = checkoutDate;
            RoomCategoryId = roomCategoryId;
            BookRate = bookRate;
            PauseBooking=false;
            Segment=segment;
            BookedBy=bookedBy;
            Discount=discount;
            Status="New";
        }

    public static Booking Create(string bookingId,GuestId guestId,RoomCategoryId roomCategoryId,
        DateTime bookDate,DateTime checkInDate, DateTime checkOutDate, Money bookRate,decimal discount,
        string segment,string bookedBy,TenantId tenantId)
      
        =>new (BookingId.Create(bookingId), guestId,roomCategoryId,bookDate, 
        checkInDate,checkOutDate,bookRate,discount,segment,bookedBy, tenantId);

    public static void AssignRoom(Booking booking, RoomId roomId)
    {
        booking.RoomId = roomId;
        booking.Status="Occupied";
    }
    public static void UpdateBookingStatus(Booking booking,string newStatus)
    =>booking.Status=newStatus;

    public static void UpdateCheckOutDate(Booking booking,DateTime checkOutDate)
    =>booking.CheckOutDate=checkOutDate;

    
}