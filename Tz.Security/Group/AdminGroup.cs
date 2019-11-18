using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tz.Data;
using Tz.ClientManager;
using System.Data;

namespace Tz.Security.Group
{   
    public class AdminGroup : SecurityGroup
    {
        private string conn;
        ClientServer ck;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="groupID"></param>
        public AdminGroup(string clientID,string groupID) :base(clientID,groupID, GetConnection(clientID))
        {
            this.ClientID = clientID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        public AdminGroup(string clientID):base(clientID, GetConnection(clientID)) {
          //  conn = GetConnection(ClientID);
            this.ClientID = clientID;
        }
        
         

      
    }

    
}
