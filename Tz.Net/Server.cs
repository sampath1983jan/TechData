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
        public string ServerName { get; set; }
        public string Host { get; set; }
        public string DBName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string ServerID { get { return _serverID; } }

        private Data.Server dServer;

        public Server() {
            ServerName = "";
            Host = "";
            _serverID = "";
            Port = 0;   
            Password = "";
            UserName = "";
        }
        public string Connection() {
            return "Server="+ this.Host +";Initial Catalog="+ this.DBName +";Uid="+ this.UserName +";Pwd="+ this.Password +"";
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
            int port,
            string serverName)
        {
            _serverID = "";
            this.Host = host;
            this.DBName = dbName;
            this.UserName = userName;
            this.Password = password;
            this.Port = port;
            this.ServerName = serverName;
        }

        public bool Test() {
            try
            {
                var a = new Data.Server(this.Connection());
                a.Test();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally {
            
            }
            return true;
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
                .Add(Tz.Data.TzAccount.Server.ServerID.Name, "ServerID",true)
                .Add(Tz.Data.TzAccount.Server.ServerName.Name, "ServerName")
                     .Add(Tz.Data.TzAccount.Server.Host.Name, "Host")
                     .Add(Tz.Data.TzAccount.Server.UserID.Name, "UserName")
                     .Add(Tz.Data.TzAccount.Server.Password.Name, "Password")
                     .Add(Tz.Data.TzAccount.Server.Port.Name, "Port")
                      .Add(Tz.Data.TzAccount.Server.DB.Name, "DBName")                    
                     , null, null).FirstOrDefault();
            this.Merge<Server>(c);
        }

        public static List<Server> GetServer() {
            DataTable dt = new DataTable();
            Data.Server   dServer = new Data.Server();
            dt = dServer.GetServer();
            if (dt.Rows.Count > 0)
            {
                return dt.toList<Server>(new DataFieldMappings()
                      .Add(Tz.Data.TzAccount.Server.ServerID.Name, "ServerID", true)
                             .Add(Tz.Data.TzAccount.Server.ServerName.Name, "ServerName")
                     .Add(Tz.Data.TzAccount.Server.Host.Name, "Host")
                     .Add(Tz.Data.TzAccount.Server.UserID.Name, "UserName")
                     .Add(Tz.Data.TzAccount.Server.Password.Name, "Password")
                     .Add(Tz.Data.TzAccount.Server.Port.Name, "Port")
                      .Add(Tz.Data.TzAccount.Server.DB.Name, "DBName")
                     , null, null).ToList();
            }
            else {
                return new List<Server>();
            }           
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
                    Port,ServerName);
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
                    Port, ServerName))
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
