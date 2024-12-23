using Hotlis.Domain.Entities.ValueObjects;
using KS.Domain.Common.Models;

namespace Hotlis.Domain.Entities;
public sealed class Tenant:Entity<TenantId>
{
    public TenantId TenantId { get;}
    public string Name { get; private set;}
    public string TenantCode { get; private set;}
    public string Email{get;private set;}
    public string Phone1{get;private set;} 
    public string? Phone2{get;private set;} 
    public string Address{get;private set;}
    public string City{get;private set;}
    public string State{get;private set;}
    public string Country{get;private set;}
    
    public string Status { get; private set;}

    #pragma warning disable
    private Tenant():base(default){}
    #pragma warning restore

    private Tenant(TenantId tenantId, string name,string tenantCode,string email, string phone1, string? phone2,
    string address, string city, string state, string country, string status):base(tenantId)
    {
        TenantId = tenantId;
        Name = name;
        TenantCode = tenantCode;
        Email = email;
        Phone1 = phone1;
        Phone2 = phone2;
        Address = address;
        City = city;
        State = state;
        Country = country;
        Status = status;
    }

    public static Tenant Create(string name,string tenantCode,string email, string phone1, string? phone2,
    string address, string city, string state, string country, string status)            
    =>new(TenantId.CreateUnique(),name,tenantCode,email,phone1,phone2,address,city,state,country,status);

}