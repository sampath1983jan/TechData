using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.Security
{
    public class UserGroup
    {
       public string UserID;
       private string RoleID;
        private string ClientID;
       public SecurityGroup Role;

        public UserGroup(string userID,string clientID,string roleID) {
            this.UserID = userID;
            this.ClientID = clientID;
            RoleID = roleID;
            Load();
        }

        public SecurityGroup GetUserGroup() {
            if (Role.GroupType == GroupBaseType.admin)
            {
                return (Group.AdminGroup)Role;
            }
            else if (Role.GroupType == GroupBaseType.superadmin)
            {
                return (Group.SuperAdminGroup)Role;
            }
            else if (Role.GroupType == GroupBaseType.systemadmin)
            {
                return (Group.SystemAdminGroup)Role;
            }
            else if (Role.GroupType == GroupBaseType.manager)
            {
                return (Group.ManagerGroup)Role;
            }
            else if (Role.GroupType == GroupBaseType.executemanager)
            {
                return (Group.ManagerGroup)Role;
            }
            else if (Role.GroupType == GroupBaseType.user)
            {
                return (Group.UserGroup)Role;
            }
            else
                return null;
        }

        private void Load() {

        }
    }
}
