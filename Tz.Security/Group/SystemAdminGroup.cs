using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tz.ClientManager;

namespace Tz.Security.Group
{
    public class SystemAdminGroup : SecurityGroup
    {
        private string conn;
        ClientServer ck;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="groupID"></param>
        public SystemAdminGroup(string clientID, string groupID) : base(clientID, groupID, GetConnection(clientID))
        {
            this.ClientID = clientID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        public SystemAdminGroup(string clientID) : base(clientID, GetConnection(clientID))
        {
            //  conn = GetConnection(ClientID);
            this.ClientID = clientID;

        }

        // system admin
        /// <summary>
        /// 
        /// </summary>
        new public void Check()
        {
            throw new NotImplementedException();
        }

        new public bool Save()
        {
            return true;
        }

        new public bool Remove()
        {
            return true;
        }
    }
}
