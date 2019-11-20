using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.Security.Group.GroupImplementor
{
   public class UserGroupImplementation : IGroupImplementor
    {
        Data.Group.SecurityGroup dSg;
        public bool AddPrivilege(string clientid,
            string groupid, 
            string componentID,
            int componentType, 
            bool add,
            bool edit, 
            bool remove,
            bool view)
        {
            IPrivilege pri;
            if ((PrivilegeType)componentType == PrivilegeType.ANALYTIC)
            {
                pri = new Privileges.AnalyticPrivilege(clientid, groupid, componentID, "", add, remove, edit, view);
                return pri.Save();
            }
            else if ((PrivilegeType)componentType == PrivilegeType.DASHBOARD)
            {
                pri = new Privileges.DashboardPrivilege(clientid, groupid, componentID, "", add, remove, edit, view);
                return pri.Save();
            }
            else if ((PrivilegeType)componentType == PrivilegeType.FEATURE)
            {
                pri = new Privileges.FeaturePrivilege(clientid, groupid, componentID, "", add, remove, edit, view);
                return pri.Save();
            }
            else if ((PrivilegeType)componentType == PrivilegeType.COMPONENT)
            {
                pri = new Privileges.ComponentPrivilege(clientid, groupid, componentID, "", add, remove, edit, view);
                return pri.Save();
            }
            else if ((PrivilegeType)componentType == PrivilegeType.REPORT)
            {
                pri = new Privileges.ReportPrivilege(clientid, groupid, componentID, "", add, remove, edit, view);
                return pri.Save();
            }
            else {
                return false;
            }
         
             
        }

        public bool Remove(string clientid, string groupID)
        {
            dSg = new Data.Group.SecurityGroup(Tz.Security.SecurityGroup.GetConnection(clientid));
            if (dSg.Remove(groupID, clientid))
            {
                dSg.RemoveAllPrivileges(groupID, clientid);
                return true;
            }
            else
            {
                return false;
            }
        }

        public string SaveGroup(string clientID, string groupID, string name, string description, Context userContext, bool isBaseType)
        {
            dSg = new Data.Group.SecurityGroup(Tz.Security.SecurityGroup.GetConnection(clientID));
            if (groupID != "")
            {
                bool val = dSg.Update(clientID, groupID,
                name,
                description,
                (int)userContext,
                true);
                return groupID;
            }
            else
            {
                string val = dSg.Save(clientID,
                name,
                description,
                (int)userContext,
                true, (int)GroupBaseType.user);
                return val;
            }
        }
    }
}
