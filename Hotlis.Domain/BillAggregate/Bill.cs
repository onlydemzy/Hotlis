using Hotlis.Domain.BillAggragate.ValueObjects;
using Hotlis.Domain.BookingAggragate.ValueObjects;
using Hotlis.Domain.Common.ValueObjects;
using Hotlis.Domain.PaymentAggragate.ValueObjects;
using Hotlis.Domain.RoomAggragate.ValueObjects;
using KS.Domain.Common.Models;

namespace Hotlis.Domain.BillAggragate;
public sealed class Bill:AggregateRoot<BillId, string>
{
    private readonly static List<PaymentId> _paymentIds=[];
    public string BilledTo{get;private set;}
    public BookingId? BookingId{get;private set;}
    public RoomId? RoomId{get;private set;}
    
    public DateTime CreatedDate{get;private set;}
    
    public string Service{get; private set;}
    public string Description{get; private set;}
    public string GeneratedBy{get;private set;}
    public Price AmountDue{get; private set;}
    public DateTime UpdatedDate{get;private set;}
    public IReadOnlyList<PaymentId> PaymentIds=> _paymentIds.AsReadOnly();

    #pragma warning disable
    private Bill():base(default){}
    #pragma warning restore

    private Bill(BillId billId, string billedTo, DateTime createdDate,string service, string description,
        string generatedBy, Price amountDue,DateTime updatedDate,BookingId? bookingId=null,
        RoomId? roomId=null):base(billId)
        {
            BilledTo=billedTo;
            CreatedDate = createdDate;
            Service = service;
            Description = description;
            AmountDue = amountDue;
            UpdatedDate = updatedDate;
            GeneratedBy=generatedBy;
            BookingId=bookingId;
            RoomId=roomId;
        }
    
    public static Bill Create(string billId, string billedTo, DateTime createdDate,string service, string description,
        string generatedBy,Price amount,DateTime updatedDate,BookingId? bookingId=null,
        RoomId? roomId=null)
        =>new(BillId.Create(billId), billedTo, createdDate,service,description, 
        generatedBy,amount, updatedDate,bookingId,roomId);

    public static void AddPayments(PaymentId paymentId)
    =>_paymentIds.Add(paymentId);

    public static void BillToRoom(Bill bill,BookingId bookingId,RoomId roomId)
    {
        bill.BookingId=bookingId;
        bill.RoomId=roomId;
    }
   
}