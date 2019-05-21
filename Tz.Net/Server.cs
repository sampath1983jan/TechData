using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.Net
{
    public class Server:INetImplimentor
    {
        private string _serverID;
        public string Host { get; set; }
        public string DBName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string ServerID { get { return _serverID; } }

        private Data.Server dServer;

        public Server() {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverID"></param>
        public Server(string serverID) {
            _serverID = serverID;
            Load();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="dbName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="port"></param>
        public Server(string host,
            string dbName,
            string userName,
            string password,
            int port)
        {
            _serverID = "";
            this.Host = host;
            this.DBName = dbName;
            this.UserName = userName;
            this.Password = password;
            this.Port = port;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Load()
        {
            DataTable dt = new DataTable();
            dServer = new Data.Server();
            dt = dServer.GetServer(this.ServerID);
            Server c = dt.toList<Server>(new DataFieldMappings()                  
                     .Add(Tz.Data.TzAccount.Server.Host.Name, "Host")
                     .Add(Tz.Data.TzAccount.Server.UserID.Name, "UserName")
                     .Add(Tz.Data.TzAccount.Server.Password.Name, "Password")
                     .Add(Tz.Data.TzAccount.Server.Port.Name, "Port")
                      .Add(Tz.Data.TzAccount.Server.DB.Name, "DBName")                    
                     , null).FirstOrDefault();
            this.Merge<Server>(c);
        }

        public bool Remove()
        {
            dServer = new Data.Server();
            return dServer.Remove(ServerID);
        }

        public bool Save()
        {
            dServer = new Data.Server();
            if (_serverID == "")
            {
                _serverID = dServer.Save(Host,
                    DBName,
                    UserName,
                    Password,
                    Port);
                if (_serverID != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {

                if (dServer.Update(ServerID,Host,
                    DBName,
                    UserName,
                    Password,
                    Port))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
