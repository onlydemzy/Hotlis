namespace KS.Domain.Entities.UserManagement;
public class AuditLog 
{ 
    public long Id { get; set; } 
    public string Action { get; set; }=string.Empty; 
    public string TableName { get; set; }=string.Empty; 
    public string KeyValues { get; set; } =string.Empty; 
    public string OriginalValues { get; set; } =string.Empty; 
    public string NewValues { get; set; } =string.Empty; 
    public DateTime TimeStamp { get; set; }
    public string UserId { get; set; } =string.Empty; // Add this line 
    public string UserName { get; set; } =string.Empty;
}