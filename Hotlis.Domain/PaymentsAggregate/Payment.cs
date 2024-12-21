using Hotlis.Domain.PaymentAggragate.ValueObjects;
using Hotlis.Domain.Common.ValueObjects;
using KS.Domain.Common.Models;
using Hotlis.Domain.BillAggragate.ValueObjects;

namespace Hotlis.Domain.PaymentAggragate;
public sealed class Payment:AggregateRoot<PaymentId, string>
{
    public BillId BillId { get; private set; }
    public DateTime PayDate{get;private set;}
    public string Service{get; private set;}
    public string Description{get; private set;}
    public Price Amount{get; private set;}
    public string PayMethod{get;private set;}
    public string SubmittedBy{get;private set;}

    #pragma warning disable
    private Payment():base(default){}
    #pragma warning restore

    private Payment(PaymentId PaymentId, BillId billId,string service,string description, 
        Price amount,DateTime payDate,string payMethod,string submittedBy):base(PaymentId)
        {
            PayDate = payDate;
            Description = description;
            Amount = amount;
            Service=service;
            BillId=billId;
            PayMethod=payMethod;
            SubmittedBy=submittedBy;
        }
    
    public static Payment Create(string paymentId, BillId billId,string service,string description, 
        Price amount,DateTime payDate,string payMethod,string submittedBy)
        =>new(PaymentId.Create(paymentId),billId,service,description,amount,
        payDate,payMethod,submittedBy);
}