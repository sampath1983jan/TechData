using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tz.Security.Group;
namespace Tz.Security
{
    public class SecurityGroupManager
    {
        
        public string ClientID { get; set; }

       public string GroupID { get; set; }

        private SecurityGroup Group { get; set; }

        public SecurityGroupManager(string clientid, string groupid) {
            this.ClientID = clientid;
            this.GroupID = groupid;
            Group = new SecurityGroup(clientid, groupid, SecurityGroup.GetConnection(clientid));            
        }

        public SecurityGroupManager(string clientid)
        {
            this.ClientID = clientid;
        }

        public SecurityGroup NewGroup(Security.GroupBaseType groupBaseType) {
            if (groupBaseType == GroupBaseType.user)
            {
                Group = new Group.UserGroup(this.ClientID);

            }
            else if (groupBaseType == GroupBaseType.systemadmin)
            {
                Group = new Group.SystemAdminGroup(this.ClientID);
            }
            else if (groupBaseType == GroupBaseType.superadmin)
            {
                Group = new Group.SuperAdminGroup(this.ClientID);
            }
            else if (groupBaseType == GroupBaseType.admin)
            {
                Group = new Group.AdminGroup(this.ClientID);
            }
            else if (groupBaseType == GroupBaseType.manager)
            {
                Group = new Group.ManagerGroup(this.ClientID);
            }
           
            return Group;
        }

        public bool UpdateGroup(string name,string description) {
            Group.Name = name;
            Group.Description = description;
            return Group.Save();
        }

        public bool AddAnalyticPrivilege(string componentID, bool add,
            bool edit, bool view, bool remove)
        {
         return   Group.AddAnalyticPrivilege(componentID,
                add, edit, remove, view);
        }

        public bool AddDashboardPrivilege(string componentID, bool add,
         bool edit, bool view, bool remove)
        {
            return Group.AddDashboardPrivilege(componentID,
                   add, edit, remove, view);
        }

        public bool AddFeaturePrivilege(string componentID, bool add,
         bool edit, bool view, bool remove)
        {
            return Group.AddFeaturePrivilege(componentID,
                   add, edit, remove, view);
        }

        public bool AddReportPrivilege(string componentID, bool add,
         bool edit, bool view, bool remove)
        {
            return Group.AddReportPrivilege(componentID,
                   add, edit, remove, view);
        }
        public bool Remove()
        {
            
         return   Group.Remove();
        }       

    }
}
