using KS.Domain.Common.Models;
using KS.Domain.UserManagement.ValueObjects;

namespace KS.Domain.Entities.UserManagement;

public class Role:Entity<RoleId>
{
    private static readonly List<User> _users=[];
    private static readonly List<Permission> _permissions=[];
    public RoleId RoleId { get;}
    public string RoleName { get;}
    

    public IReadOnlyList<Permission> Permissions => _permissions.AsReadOnly();
    public IReadOnlyList<User> Users=>_users.AsReadOnly();
    
    private Role(RoleId roleId, string roleName):base(roleId)
    {
        RoleId = roleId;
        RoleName = roleName;
    }
    #pragma warning disable
    private Role(){}
    #pragma warning restore
    public static Role Create(string roleName)
        =>new(RoleId.CreateUnique(),roleName);

    public static void AddUser(User user,Role role)
    {
        if(!role.Users.Contains(user))
        {
            _users.Add(user);
        }
    }
    public static void RemoveUser(User user)
    {
        if(_users.Contains(user))
        {
            _users.Remove(user);
        }
    }

    public static void AddPermission(Permission permission)
    {
        if(!_permissions.Contains(permission))
        {
            _permissions.Add(permission);
        }
    }
    public static void RemovePermission(Permission permission) 
    {
        if(_permissions.Contains(permission))
        {
            _permissions.Remove(permission);
        }
    }

     
}