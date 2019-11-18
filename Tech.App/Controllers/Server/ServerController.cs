using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tz.ClientManager;
namespace Tz.BackApp.Controllers.Server
{
    public class ServerController : Controller
    {
        // GET: Server
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Server()
        {
            return View();
        }              

        public JsonpResult Save(string host,
            string dbName,
            string userName,
            string password,
            int port,
            string serverName
            ) {
            ClientManager. Server s = new ClientManager.Server(host, dbName, userName, password, port,serverName);
            s.Save();
            return new JsonpResult(s.ServerID);
        }
        public JsonpResult Update(string serverid,string host,
          string dbName,
          string userName,
          string password,
          int port,
          string serverName
          )
        {
            ClientManager. Server s = new ClientManager.Server(serverid);
            s.ServerName = serverName;
            s.Host = host;
            s.DBName = dbName;
            s.UserName = userName;
            s.Port = port;
            s.Save();
            return new JsonpResult(s.ServerID);
        }
        public JsonpResult Test(string host,
            string dbName,
            string userName,
            string password,
            int port,
            string serverName)
        {
            ClientManager. Server s = new ClientManager.Server(host, dbName, userName, password, port, serverName);
            return new JsonpResult(s.Test());
        }

        public JsonpResult Gets() {
            return new JsonpResult(ClientManager.Server.GetServer());
        }
        public JsonpResult Get(string serverID)
        {
            return new JsonpResult(  new ClientManager.Server(serverID));
        }
    }
}