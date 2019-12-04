using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tz.Core;

namespace Tz.BackApp.Controllers.App
{
    public class AppController : Controller
    {
         
        public ActionResult Index()
        {
            return View();
        }
       
        public JsonpResult Get() {
            string clientid = Request.Params["clientkey"];
           return new JsonpResult( Tz.App.AppManager.GetAppManagers(clientid));
        }

        public JsonpResult Save( string Name,string description) {
            string clientid = Request.Params["clientkey"];
            var tzApp = new Tz.App.AppManager(clientid);
            tzApp.AppName = Name;
            tzApp.Description = description;
            tzApp.Category = "None";
            return new JsonpResult(tzApp.Save());
        }
        public ActionResult GotoHome(string id) {
            return RedirectToAction("AppInfo", "App",id);
        }
        public ActionResult AppInfo(string id)
        {
            ViewBag.AppID =  id ;
            return View();
        }
        [Route("MyApp/{appid}")]
        public JsonpResult GetApp( string appid) {
            string clientid = Request.Params["clientkey"];
            return new JsonpResult(new Tz.App.AppManager(clientid, appid));
        }
        [Route("App/{appid}/Remove")]
        public JsonpResult RemoveApp( string appid)
        {
            string clientid = Request.Params["clientkey"];
            return new JsonpResult(new Tz.App.AppManager(clientid, appid).Remove());
        }
        //[Route("App/{appid}/Component/Create")]
        //public JsonpResult SaveComponent(string appid, string compName, string title) {
        //    string clientid = Request.Params["clientkey"];        
        //    Core.ComponentManager mg = new ComponentManager((Core.ComponentType.core),
        //        clientid, compName, "", title, "");           
        //    return new JsonpResult(mg.SaveComponent());
        //}
    }
}