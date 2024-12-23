using Hotlis.Domain.PaymentAggragate.ValueObjects;
using Hotlis.Domain.Common.ValueObjects;
using KS.Domain.Common.Models;
using Hotlis.Domain.BillAggragate.ValueObjects;
using Hotlis.Domain.Entities.ValueObjects;

namespace Hotlis.Domain.PaymentAggragate;
public sealed class Payment:AggregateRoot<PaymentId, string>
{
    public BillId BillId { get; private set; }
    public DateTime PayDate{get;private set;}
    public string Service{get; private set;}
    public string Description{get; private set;}
    public Money Amount{get; private set;}
    public string PayMethod{get;private set;}
    public string SubmittedBy{get;private set;}

    #pragma warning disable
    private Payment():base(default,default){}
    #pragma warning restore

    private Payment(PaymentId PaymentId, BillId billId,string service,string description, 
        Money amount,DateTime payDate,string payMethod,string submittedBy,TenantId tenantId):base(PaymentId,tenantId)
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
        Money amount,DateTime payDate,string payMethod,string submittedBy,TenantId tenantId)
        =>new(PaymentId.Create(paymentId),billId,service,description,amount,
        payDate,payMethod,submittedBy,tenantId);
}