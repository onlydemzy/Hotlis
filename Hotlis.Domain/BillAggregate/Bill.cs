using Hotlis.Domain.BillAggragate.ValueObjects;
using Hotlis.Domain.BookingAggragate.ValueObjects;
using Hotlis.Domain.Common.ValueObjects;
using Hotlis.Domain.Entities.ValueObjects;
using Hotlis.Domain.GuestAggragate.ValueObjects;
using Hotlis.Domain.PaymentAggragate.ValueObjects;
using Hotlis.Domain.RoomAggragate.ValueObjects;
using Hotlis.Domain.ServiceConsumedAggragate.ValueObjects;
using KS.Domain.Common.Models;

namespace Hotlis.Domain.BillAggragate;
public sealed class Bill:AggregateRoot<BillId, string>
{
    private readonly static List<PaymentId> _paymentIds=[];
    public string BilledTo{get;private set;}
    public BookingId? BookingId{get;private set;}
    public RoomId? RoomId{get;private set;}
    public GuestId? GuestId{get;private set;}
    public ConsumedServiceId ConsumedServiceId { get; private set; }
    public string Description { get; private set; }
    public Money AmountDue { get; set;}
    public DateTime CreatedDate{get;private set;}
    public DateTime UpdatedDate { get; private set; }
    public IReadOnlyList<PaymentId> PaymentIds=>_paymentIds.AsReadOnly();

#pragma warning disable
    private Bill():base(default,default){}
    #pragma warning restore

    private Bill(BillId billId, string billedTo,ConsumedServiceId consumedServiceId,string description,
        DateTime updatedDate,Money amountDue,TenantId tenantId,BookingId? bookingId=null, 
        RoomId? roomId=null,GuestId? guestId=null):base(billId,tenantId)
        {
            BilledTo=billedTo;
            CreatedDate = DateTime.UtcNow;
            Description = description;
            AmountDue = amountDue;
            UpdatedDate = updatedDate;
            BookingId=bookingId;
            RoomId=roomId;
            ConsumedServiceId=consumedServiceId;
        }
    
    public static Bill Create(string billId, string billedTo, ConsumedServiceId consumedServiceId, string description,
        DateTime updatedDate, Money amountDue, TenantId tenantId, BookingId? bookingId = null,
        RoomId? roomId = null,GuestId? guestId=null)
        {
        string id;
        if (billId is null)
        {
            id = Guid.NewGuid().ToString();
        }
        else
        {
            id = billId;
        }
        return new(BillId.Create(id), billedTo, consumedServiceId, description, 
        updatedDate, amountDue, tenantId, bookingId, roomId,guestId);
    }
        

    public static void AddPayments(PaymentId paymentId)
    =>_paymentIds.Add(paymentId);

    public static void BillToRoom(Bill bill,BookingId bookingId,RoomId roomId)
    {
        bill.BookingId=bookingId;
        bill.RoomId=roomId;
    }
   
}