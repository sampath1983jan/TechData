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

        [Route("App/{appid}/Component/{compid}/Atrribute/Add")]
        public JsonpResult SaveAttribute(string appid,string compid ,string attribute) {
            string clientid = Request.Params["clientkey"];
            var apm = new Tz.App.AppManager(clientid, appid);
            apm.GetComponents();
           var comp= apm.GetComponent(compid);
            Core.ComponentManager mg = new ComponentManager(comp);           

            Models.Attribute att = new Models.Attribute();
            att = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Attribute>(attribute);
          //  Core.ComponentManager mg = new ComponentManager(clientid, compid);
            var natt = new ComponentAttribute(clientid)
            {
                AttributeName = att.AttributeName,
                AttributeType = (Core.ComponentAttribute.ComoponentAttributeType)att.AttributeType,
                ClientID = clientid,
                DefaultValue = att.DefaultValue,
                FileExtension = att.FileExtension,
                IsAuto = att.IsAuto,
                IsCore = att.IsCore,
                IsNullable = att.IsNullable,
                IsPrimaryKey = att.IsPrimaryKey,
                IsReadOnly = att.IsReadOnly,
                IsRequired = att.IsRequired,
                IsSecured = att.IsSecured,
                IsUnique = att.IsUnique,
                LookUpID =att.LookUpID,
                Length = att.Length,
                RegExp = att.RegExp
            };
            return new JsonpResult(mg.AddAttribute(natt));
        }


 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="componentID"></param>
        /// <returns></returns>
        [Route("App/{appid}/Component/{compid}/change/{aid}")]
        public JsonpResult GetAttribute(string appid, string compid,string aid)
        {
            //   Tz.Net.ClientServer c = new Net.ClientServer(clientid);
            //Tz.Core.ComponentManager cm = new Core.ComponentManager(clientid, componentID);
            //return new JsonpResult(cm);
            string clientid = Request.Params["clientkey"];
            var a = new Tz.App.AppManager(clientid, appid);
            a.GetComponents();
            var comp = a.GetComponent(compid);
            comp.LoadAttributes();
            var all= comp.Attributes.Where(x => x.FieldID == aid).FirstOrDefault();
            return new JsonpResult(all);
        }

        [Route("App/{appid}/Component/{compid}/publish")]
        public JsonpResult PublishComponent(string appid, string compid) {
            string clientid = Request.Params["clientkey"];
            var a = new Tz.App.AppManager(clientid, appid);
            return new JsonpResult( a.PublishComponent(compid));
        }
        #region Form
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="formname"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [Route("App/{appid}/Form/CreateForm")]
        public JsonpResult SaveMainForm(string appid,string formname,
            string description) {
            string clientid = Request.Params["clientkey"];
            var a = new Tz.App.AppManager(clientid, appid);
            var f=a.NewForm();
            f.Name = formname;
            f.Description = description;
            return new JsonpResult(a.SaveForm(f));
        }

        [Route("App/{appid}/Form/Get")]
        public JsonpResult GetForms (string appid)
        {
            string clientid = Request.Params["clientkey"];
            var a = new Tz.App.AppManager(clientid, appid);
            a.LoadForms();
            return new JsonpResult(a);
        }

        [Route("App/{appid}/Form/Get/{formid}")]
        public JsonpResult GetForms(string appid,string formid)
        {
            string clientid = Request.Params["clientkey"];
            var a = new Tz.App.AppManager(clientid, appid);
            a.LoadForms();
         var fm=   a.GetForm(formid);
            fm.LoadFormFields();
            return new JsonpResult(fm);
        }

        [Route("App/{appid}/Form/AssignComponent/{formid}")]
        public JsonpResult AssignComponent(string appid, string formid,string compID)
        {
            string clientid = Request.Params["clientkey"];
            var a = new Tz.App.AppManager(clientid, appid);
            a.LoadForms();
            var fm = a.GetForm(formid);        
            return new JsonpResult(fm.SaveComponent(compID));
        }

        #endregion

        #region Lookup
        [Route("App/{appid}/Lookup/Create")]
        public JsonpResult CreateLookup(string appid, string lookupname, string description, bool isCore) {
            string clientid = Request.Params["clientkey"];
            var a = new Tz.App.AppManager(clientid, appid);
            var lok =a.NewLookup();
            lok.Name = lookupname;
            lok.Description = description;
            lok.IsCore = isCore;
            return new JsonpResult(a.SaveLookup(lok));
        }
        [Route("App/{appid}/Lookup/get")]
        public JsonpResult GetLookups(string appid)
        {
            string clientid = Request.Params["clientkey"];
            var a = new Tz.App.AppManager(clientid, appid);
            a.LoadLookUp();
            return new JsonpResult( a);
        }
        #endregion

        //[Route("App/{appid}/Component/Create")]
        //public JsonpResult SaveComponent(string appid, string compName, string title) {
        //    string clientid = Request.Params["clientkey"];        
        //    Core.ComponentManager mg = new ComponentManager((Core.ComponentType.core),
        //        clientid, compName, "", title, "");           
        //    return new JsonpResult(mg.SaveComponent());
        //}
    }
}