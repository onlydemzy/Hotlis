using KS.Domain.Common.Models;
using KS.Domain.UserManagement.ValueObjects;

namespace KS.Domain.Entities.UserManagement;
public class Permission:Entity<PermissionId>
{
    
    private readonly List<Role> _roles=[];
    public PermissionId PermissionId { get;}
    public string Resource { get;}
    public string Module{get;}
    
    public IReadOnlyList<Role> Roles=> _roles.AsReadOnly();
    private Permission(PermissionId permissionId,string resource, string module):base(permissionId) 
    {
        Resource = resource;
        Module = module;
        PermissionId=permissionId;
    }
    #pragma warning disable
    private Permission(){}
    #pragma warning restore

    public static Permission Create(string resource, string module)
    =>new(PermissionId.CreateUnique(),resource, module);
    
}