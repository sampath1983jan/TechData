using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tz.BackApp.Models;

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
            Tz.Net.Client c = new Net.Client(clientid);
            return new JsonpResult(c);
        }

        [HttpGet]
        public JsonpResult Gets(string obj)
        {
            if (obj != null)
            {
                var gp = Newtonsoft.Json.JsonConvert.DeserializeObject<GridParam>(obj);
                return new JsonpResult(Net.Client.GetClients());
            }
            else {
                return new JsonpResult(Net.Client.GetClients());
            }                              
        }

        [HttpGet]
        public JsonpResult GetClients(string search,int start)
        {
            if (search != null)
            {
                // var gp = Newtonsoft.Json.JsonConvert.DeserializeObject<GridParam>(obj);\
               var c= Net.Client.GetClients().Where(x => x.ClientName.IndexOf(search) >= 0);

                return new JsonpResult(c);
            }
            else
            {
                return new JsonpResult(Net.Client.GetClients());
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
            Tz.Net.Client c = new Net.Client(clientName,
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
            Tz.Net.Client c = new Net.Client(clientID);
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
            Tz.Net.ClientServer cs = new Net.ClientServer(clientid,serverid);
            return new JsonpResult(cs.Assign());
        }

        public JsonpResult RemoveServer(string clientid, string serverid) {
            Tz.Net.ClientServer cs = new Net.ClientServer(clientid, serverid);
            return new JsonpResult(cs.Remove());
        }

        public JsonpResult GetServer(string clientid) {
            Tz.Net.ClientServer cs = new Net.ClientServer(clientid);
            return new JsonpResult(cs.GetServer());
        }
    }
}