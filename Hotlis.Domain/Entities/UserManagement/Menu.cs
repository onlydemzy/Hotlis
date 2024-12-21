using KS.Domain.Common.Models;
using KS.Domain.UserManagement.ValueObjects;

namespace KS.Domain.Entities.UserManagement;

    public class Menu:Entity<MenuId>
    {
        
        private readonly List<Menu> _childrenMenus=[];
        public MenuId MenuId { get;}
        public string Name { get; private set; }//Display name
        public string Resource { get; private set; }
        public string Collapse { get; private set; }
        public string Heading { get; private set; }
        public string Icon { get; private set; }
        public byte MenuOrder { get; private set; }
        public PermissionId PermissionId { get; private set; }
        public bool AlwaysEnable { get; private set; }
        
        public MenuId? ParentMenuId{get;private set;}
        public IReadOnlyList<Menu> ChildrenMenus=>_childrenMenus.AsReadOnly();
        

        private Menu(MenuId menuId, string name, string resource,string collapse,string heading,
            string icon, byte menuOrder,PermissionId permissionId,
            bool alwaysEnable,MenuId? parentMenuId=null):base(menuId)
            {
                MenuId=menuId;
                Name=name;
                Resource=resource;
                Collapse=collapse;
                Heading=heading;
                Icon=icon;
                MenuOrder=menuOrder;
                PermissionId=permissionId;
                AlwaysEnable=alwaysEnable;
                ParentMenuId=parentMenuId;

            }

        #pragma warning disable
        private Menu():base(default){}
        #pragma warning restore

        public static Menu Create(string name, string resource,string collapse,string heading,
            string icon, byte menuOrder,PermissionId permissionId,
            bool alwaysEnable,MenuId? parentMenuId)
        =>new (MenuId.CreateUnique(),name,resource,collapse,heading,icon,
            menuOrder,permissionId,alwaysEnable,parentMenuId);
    }
