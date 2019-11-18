using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tz.BackApp.Models;
using Tz.ClientManager;
namespace Tz.BackApp.Controllers.Client
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Client()
        {
            ViewBag.Message = "Your Client page.";
            return View();
        }
        [HttpGet]
        public JsonpResult Get(string clientid) {
         Tz.ClientManager.Client c = new Tz.ClientManager.Client(clientid);
            return new JsonpResult(c);
        }

        [HttpGet]
        public JsonpResult Gets(string obj)
        {
            if (obj != null)
            {
                var gp = Newtonsoft.Json.JsonConvert.DeserializeObject<GridParam>(obj);
                return new JsonpResult(Tz.ClientManager.Client.GetClients());
            }
            else {
                return new JsonpResult(Tz.ClientManager.Client.GetClients());
            }                              
        }

        [HttpGet]
        public JsonpResult GetClients(string search,int start)
        {
            if (search != null)
            {
                // var gp = Newtonsoft.Json.JsonConvert.DeserializeObject<GridParam>(obj);\
               var c= Tz.ClientManager.Client.GetClients().Where(x => x.ClientName.IndexOf(search) >= 0);

                return new JsonpResult(c);
            }
            else
            {
                return new JsonpResult(Tz.ClientManager.Client.GetClients());
            }
        }

        public JsonpResult Save(string clientName,
            string clientNo,
            string orgName,
            string address,
            string state,
            string country,
            string email,
            string hostname
            ) {
            Tz.ClientManager.Client c = new Tz.ClientManager.Client(clientName,
                clientNo,
                address,
                state,
                country,
                email,
                "",
                orgName,
                hostname,
                true);
            c.Save();           
            return new JsonpResult(c.ClientID);
        }

        public JsonpResult Update(string clientID
            ,string clientName,
            string clientNo,
            string orgName,
            string address,
            string state,
            string country,
            string email,
            string hostname
            )
        {
            Tz.ClientManager.Client c = new Tz.ClientManager.Client(clientID);
            c.ClientName = clientName;
            c.ClientNo = clientNo;
            c.OrganizationName = orgName;
            c.Address = address;
            c.State = state;
            c.Country = country;
            c.Email = email;
            c.ClientHost = hostname;
            c.Save();
            return new JsonpResult(c.ClientID);
        }

        public JsonpResult Assign(string clientid, string serverid) {
            Tz.ClientManager.ClientServer cs = new Tz.ClientManager.ClientServer(clientid,serverid);
            return new JsonpResult(cs.Assign());
        }

        public JsonpResult RemoveServer(string clientid, string serverid) {
            Tz.ClientManager.ClientServer cs = new Tz.ClientManager.ClientServer(clientid, serverid);
            return new JsonpResult(cs.Remove());
        }

        public JsonpResult GetServer(string clientid) {
            Tz.ClientManager.ClientServer cs = new Tz.ClientManager.ClientServer(clientid);
            return new JsonpResult(cs.GetServer());
        }
    }
}