using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.Security.Group.GroupImplementor
{
  public  class ManagerGroupImplementation: IGroupImplementor
    {
        Data.Group.SecurityGroup dSg;
        public bool AddPrivilege(string clientid, string groupid, string ComponentID, int componentType, bool add, bool edit, bool remove, bool view)
        {
            throw new NotImplementedException();
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
                true, (int)GroupBaseType.manager);
                return val;
            }
        }
    }
}
