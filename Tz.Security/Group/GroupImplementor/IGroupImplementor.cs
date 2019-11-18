using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.Security.Group
{

    public interface IGroupImplementor
    {
        string SaveGroup(string clientID, string groupID, string name, string description,
            Tz.Security.Context userContext,             
            bool isBaseType);
        bool Remove(string clientid,string groupID);
        bool AddPrivilege(string clientid,
            string groupid,
            string ComponentID,
            int componentType, 
            bool add, 
            bool edit, 
            bool remove,
            bool view);
    }
}
    