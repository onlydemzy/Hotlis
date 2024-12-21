using System.Security.Cryptography;
using System.Text;
using KS.Domain.Common.Models;
using KS.Domain.UserManagement.ValueObjects;

namespace KS.Domain.Entities.UserManagement;

public class User:Entity<UserId>
{
    private readonly List<Role> _roles=[];
    public UserId UserId{ get;}
    public string Fullname{get;private set;}
    public string Username { get;private set;}
    public string Password { get; private set;}
    public string Email { get; private set;}
    public string? Department{get; private set;}
    public bool IsActive{get; private set;}
    public string? TenantId{ get; private set;}

    public IReadOnlyList<Role> Roles=>_roles.AsReadOnly();
    

    private User(UserId userId,string username, string fullname,string password,string email,string? department,bool isaAtive, string? tenantId)
    :base(userId)
    {
        
        Username = username;
        Password = password;
        Email = email;
        Department = department;
        UserId = userId;
        Fullname=fullname;
        IsActive=isaAtive;
        TenantId=tenantId;
    }
    #pragma warning disable
    private User():base(default){}
    #pragma warning restore

    public static User Create(string userId,string username, string fullname,string password,string email,string? department,bool isaAtive,
    string? tenantId=null)
    {
        UserId id;
        
        if(userId is null)
        {
            id=UserId.CreateUnique();
        }
        else{
            id=UserId.Create(userId);
        }
        var user=new User(id,username,fullname,HashPassword(password),email,department,isaAtive,tenantId);
        
        return user;
    }
    public static void ChangePassword(User user,string newPassword)
    {
         user.Password=newPassword;
    }

    public static string HashPassword(string password)
        {
            string salt = GenerateSalt();
            string hash = Sha512Hex(salt + password);

            return salt + hash;
        }
        private static string GenerateSalt()
        {
            var random = new Random();
            byte[] salt = new byte[64];//512 bits
            random.NextBytes(salt);
            return BytesToHex(salt);

        }
        private static string Sha512Hex(string toHash)
        {
            HashAlgorithm sha = SHA3_512.Create();
            byte[] utf8 = UTF8Encoding.UTF8.GetBytes(toHash);
            return BytesToHex(sha.ComputeHash(utf8));
        }


        private static string BytesToHex(byte[] toConvert)
        {
            StringBuilder sbuilder = new (toConvert.Length * 2);
            foreach (byte b in toConvert)
            {
                sbuilder.Append(b.ToString("x2"));
            }
            return sbuilder.ToString();
        }

        public static bool ValidateUserPassword(string password, string correctHash)
        {
            if (correctHash.Length < 256)
            {
                throw new ArgumentException("Correct hash from db must be 256");
            }
            string saltComponent = correctHash[..128];
            //string saltComponent = correctHash.Substring(0, 128);
            string hashComponent = correctHash.Substring(128, 128);

            string inputtedPasswordHash = Sha512Hex(saltComponent + password);

            return string.Compare(hashComponent, inputtedPasswordHash) == 0;
        }
}