using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Tz.ClientManager
{
    public class ClientServer
    {
        public string ClientID;
        public string ServerID;
        private Data.ClientSever dClientServer;
        public ClientServer(string clientID, string serverID) {
            ClientID = clientID;
            ServerID = serverID;
        }
        public ClientServer(string clientID) {
            ClientID = clientID;
            ServerID = "";
        }
        public bool Remove() {
            dClientServer = new Data.ClientSever();
            return dClientServer.Remove(ClientID,ServerID);
        }
        public bool Assign() {
            dClientServer = new Data.ClientSever();
            DataTable dt = dClientServer.GetServer(this.ClientID);
            if (dt.Rows.Count > 0)
            {
                if (dClientServer.UpdateClientSever(this.ClientID, this.ServerID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else {
                if (dClientServer.AssignClientServer(this.ClientID, this.ServerID))
                {
                    return true;
                }
                else {
                    return false;
                }
            }

        }
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public Server GetServer() {
            dClientServer = new Data.ClientSever();
            DataTable dt= dClientServer.GetServer(this.ClientID);
            foreach (DataRow dr in dt.Rows) {
                if (dr["ServerID"] != null) {
                    this.ServerID = dr["ServerID"].ToString();
                }
            }
            if (this.ServerID != "")
            {
                return new Server(this.ServerID);
            }
            else {
                return new Server();
            }
        }        
    }


}
