using Hotlis.Domain.Entities.ValueObjects;
using Hotlis.Domain.GuestAggragate.ValueObjects;
using KS.Domain.Common.Models;

namespace Hotlis.Domain.GuestAggragate;
public sealed class Guest:AggregateRoot<GuestId,string>
{
    public string Title{get;private set;}
    public string Lastname{get;private set;}
    public string Othernames{get;private set;}
    public string Gender{get;private set;}
    public DateTime? DOB{get;private set;}
    public string Phone{get;private set;}
    public string Email{get;private set;}
    public string Address{get;private set;}
    public string Country{get;private set;}
    public string State{get;private set;}
    public string City{get;private set;}
    public string IdType{get;private set;}
    public string IdNumber{get;private set;}
    public string IdPath{get;private set;}
    public string PhotoPath{get;private set;}

    #pragma warning disable
    private Guest():base(default,default){}
    #pragma warning restore

    private Guest(GuestId guestId,string title, string lastname,string othernames,string gender,
        DateTime? dob, string phone,string email, string address,string country,string state, 
        string city,string idType,string idPath,string idNumber,
        string photoPath,TenantId tenantId):base(guestId,tenantId)
        {
            Title=title;
            Lastname=lastname;
            Othernames=othernames;
            Gender=gender;
            Phone=phone;
            Email=email;
            Address=address;
            Country=country;
            State=state;
            City=city;
            IdType=idType;
            IdPath=idPath;
            IdNumber=idNumber;
            PhotoPath=photoPath;
            DOB=dob;

        }

        public static Guest Create(string guestId,string title, string lastname,string othernames,
            string gender, DateTime? dob, string phone,string email, string address,
            string country, string state, string city,string idType,string idPath,
            string idNumber,string photoPath, TenantId tenantId)
                =>new (GuestId.Create(guestId),title,lastname,othernames,gender,dob,phone,email,
                address,country,state,city,idType,idPath,idNumber,photoPath,tenantId);

   
    
}