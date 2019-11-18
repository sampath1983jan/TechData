using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tz.Global.TzAccount;

namespace Tz.Security.Group
{
    public class SuperAdminGroup : SecurityGroup
    {
        private string conn;
        ClientServer ck;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="groupID"></param>
        public SuperAdminGroup(string clientID, string groupID) : base(clientID, groupID, GetConnection(clientID))
        {
            this.ClientID = clientID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        public SuperAdminGroup(string clientID) : base(clientID, GetConnection(clientID))
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
