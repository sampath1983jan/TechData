using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            Tz.Net.Server s = new Net.Server(host, dbName, userName, password, port,serverName);
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
            Tz.Net.Server s = new Net.Server(serverid);
            s.ServerName = serverName;
            s.Host = host;
            s.DBName = dbName;
            s.UserName = userName;
            s.Port = port;
            s.Save();
            return new JsonpResult(s.ServerID);
        }

        public JsonpResult Gets() {
            return new JsonpResult(Tz.Net.Server.GetServer());
        }
        public JsonpResult Get(string serverID)
        {
            return new JsonpResult(  new Net.Server(serverID));
        }
    }
}