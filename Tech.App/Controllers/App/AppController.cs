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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonpResult Get() {
            string clientid = Request.Params["clientkey"];
            return new JsonpResult(Tz.App.AppManager.GetAppManagers(clientid));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public JsonpResult Save(string Name, string description) {
            string clientid = Request.Params["clientkey"];
            var tzApp = new Tz.App.AppManager(clientid);
            tzApp.AppName = Name;
            tzApp.Description = description;
            tzApp.Category = "None";
            return new JsonpResult(tzApp.Save());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GotoHome(string id) {
            return RedirectToAction("AppInfo", "App", id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AppInfo(string id)
        {
            ViewBag.AppID = id;
            return View();
        }
        [Route("MyApp/{appid}")]
        public JsonpResult GetApp(string appid) {
            string clientid = Request.Params["clientkey"];
            return new JsonpResult(new Tz.App.AppManager(clientid, appid));
        }
        [Route("App/{appid}/Remove")]
        public JsonpResult RemoveApp(string appid)
        {
            string clientid = Request.Params["clientkey"];
            return new JsonpResult(new Tz.App.AppManager(clientid, appid).Remove());
        }
        [Route("App/{appid}/Component/{compid}/Atrribute/Add")]
        public JsonpResult SaveAttribute(string appid, string compid, string attribute) {
            string clientid = Request.Params["clientkey"];
            var apm = new Tz.App.AppManager(clientid, appid);
            apm.GetComponents();
            var comp = apm.GetComponent(compid);
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
                LookUpID = att.LookUpID,
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
        public JsonpResult GetAttribute(string appid, string compid, string aid)
        {
            //   Tz.Net.ClientServer c = new Net.ClientServer(clientid);
            //Tz.Core.ComponentManager cm = new Core.ComponentManager(clientid, componentID);
            //return new JsonpResult(cm);
            string clientid = Request.Params["clientkey"];
            var a = new Tz.App.AppManager(clientid, appid);
            a.GetComponents();
            var comp = a.GetComponent(compid);
            comp.LoadAttributes();
            var all = comp.Attributes.Where(x => x.FieldID == aid).FirstOrDefault();
            return new JsonpResult(all);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="compid"></param>
        /// <returns></returns>
        [Route("App/{appid}/Component/{compid}/publish")]
        public JsonpResult PublishComponent(string appid, string compid) {
            string clientid = Request.Params["clientkey"];
            var a = new Tz.App.AppManager(clientid, appid);
            return new JsonpResult(a.PublishComponent(compid));
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
        public JsonpResult SaveMainForm(string appid, string formname,
            string description) {
            string clientid = Request.Params["clientkey"];
            var a = new Tz.App.AppManager(clientid, appid);
            var f = a.NewForm();
            f.Name = formname;
            f.Description = description;
            return new JsonpResult(a.SaveForm(f));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        [Route("App/{appid}/Form/Get")]
        public JsonpResult GetForms(string appid)
        {
            string clientid = Request.Params["clientkey"];
            var a = new Tz.App.AppManager(clientid, appid);
            a.LoadForms();
            return new JsonpResult(a);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="formid"></param>
        /// <returns></returns>
        [Route("App/{appid}/Form/Get/{formid}")]
        public JsonpResult GetForms(string appid, string formid)
        {
            string clientid = Request.Params["clientkey"];
            var a = new Tz.App.AppManager(clientid, appid);
            a.LoadForms();
            var fm = a.GetForm(formid);
            fm.LoadFormFields();
            return new JsonpResult(fm);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="formid"></param>
        /// <param name="compID"></param>
        /// <returns></returns>
        [Route("App/{appid}/Form/AssignComponent/{formid}")]
        public JsonpResult AssignComponent(string appid, string formid, string compID)
        {
            string clientid = Request.Params["clientkey"];
            var a = new Tz.App.AppManager(clientid, appid);
            a.LoadForms();
            var fm = a.GetForm(formid);
            return new JsonpResult(fm.SaveComponent(compID));
        }
        #endregion
        #region Lookup
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="lookupname"></param>
        /// <param name="description"></param>
        /// <param name="isCore"></param>
        /// <returns></returns>
        [Route("App/{appid}/Lookup/Create")]

        public JsonpResult CreateLookup(string appid, string lookupname, string description, bool isCore) {
            string clientid = Request.Params["clientkey"];
            var a = new Tz.App.AppManager(clientid, appid);
            var lok = a.NewLookup();
            lok.Name = lookupname;
            lok.Description = description;
            lok.IsCore = isCore;
            return new JsonpResult(a.SaveLookup(lok));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        [Route("App/{appid}/Lookup/get")]
        public JsonpResult GetLookups(string appid)
        {
            string clientid = Request.Params["clientkey"];
            var a = new Tz.App.AppManager(clientid, appid);
            a.LoadLookUp();
            return new JsonpResult(a);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="lookupid"></param>
        /// <param name="label"></param>
        /// <param name="shortname"></param>
        /// <param name="value"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [Route("App/{appid}/{lookupid}/create")]
        public JsonpResult CreateLookUpItem(string appid, string lookupid, string label, string shortname, string value, string description) {
            string clientid = Request.Params["clientkey"];
            var a = new Tz.App.AppManager(clientid, appid);
            a.LoadLookUp();
            var lk = a.GetLookUp(lookupid);
            return new JsonpResult(lk.SaveLookupItem(label, shortname, description, "", value));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="lookupid"></param>
        /// <returns></returns>
        [Route("App/{appid}/{lookupid}/get")]
        public JsonpResult GetLookUpItems(string appid, string lookupid)
        {
            string clientid = Request.Params["clientkey"];
            var a = new Tz.App.AppManager(clientid, appid);
            a.LoadLookUp();
            return new JsonpResult(a.GetLookUp(lookupid));
        }
        #endregion

        #region Form
        [Route("App/{appid}/Form/changeshape/{formid}")]
        public JsonpResult ChangeShape(string appid, string formid, string fieldPosition) {
            string clientid = Request.Params["clientkey"];
            var a = new Tz.App.AppManager(clientid, appid);
            a.LoadForms();
         var fm=   a.GetForm(formid);
            fm.LoadFormFields();
            var fposition = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FieldPosition>>(fieldPosition);
            foreach (FieldPosition fp in fposition) {
                var ff = fm.FormFields.Where(x => x.FormFieldID == fp.FieldID || x.Attribute.FieldAttribute.FieldID == fp.FieldID ).FirstOrDefault();
                if (ff != null)
                {
                    ff.Attribute.Top = fp.Top;
                    ff.Attribute.Left = fp.Left;
                    ff.Attribute.Width = fp.Width;
                    ff.Attribute.Height = fp.Height;
                    ff.ChangeShape() ;
                }
                else
                {
                   
                }
            }

            return new JsonpResult("true");


        }

        [Route("App/{appid}/Form/createfield/{formid}")]
        public JsonpResult AssignFormField(string appid, string formid, int renderType,string attributeID,string formelement) {
            string clientid = Request.Params["clientkey"];
            var a = new Tz.App.AppManager(clientid, appid);
            a.LoadForms();
            var  fm=a.NewField(formid, (Tz.UIForms.RenderType) renderType,attributeID);
            var fAtt = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.FormField>(formelement);
            fm.Attribute.Top = fAtt.Top;
            fm.Attribute.Height = fAtt.Height;
            fm.Attribute.Left = fAtt.Left;
            fm.Attribute.Width = fAtt.Width;
            fm.Attribute.Category = (Tz.UIForms.RenderCategory) fAtt.Category;
            fm.FormFieldID = fAtt.FormFieldID;
            fm.Attribute.EnableLimit = fAtt.EnableLimit;           
            switch ((Tz.UIForms.RenderType)renderType)
            {
                case Tz.UIForms.RenderType.BOOLEAN:
                     Tz.UIForms.FormFields.Boolean ab= (Tz.UIForms.FormFields.Boolean)fm;
                    return new JsonpResult(a.AddFormField(formid, ab));
                case Tz.UIForms.RenderType.NUMBER:
                    Tz.UIForms.FormFields.Numeric num = (Tz.UIForms.FormFields.Numeric)fm;
                    num.Min = fAtt.Min;
                    num.Max = fAtt.Max;
                    num.DisplayFormat = fAtt.DisplayFormat;
                    return new JsonpResult(a.AddFormField(formid, num));
                case Tz.UIForms.RenderType.PICKER:
                    Tz.UIForms.FormFields.Date pick = (Tz.UIForms.FormFields.Date)fm;
                    pick.DateFormat = fAtt.DateFormat;
                    pick.TimeFormat = fAtt.TimeFormat;
                    pick.DateTimeFormat = fAtt.DateTimeFormat;
                    pick.pickerInterval = fAtt.pickerInterval;
                    pick.AllowedDays = fAtt.AllowedDays;
                    pick.AllowedFromHours = fAtt.AllowedFromHours;
                    pick.AllowedToHours = fAtt.AllowedToHours;
                    return new JsonpResult(a.AddFormField (formid,pick));                     
                case Tz.UIForms.RenderType.SELECTION:
                    Tz.UIForms.FormFields.Selection  sel= (Tz.UIForms.FormFields.Selection)fm;
                    sel.SeletionType = (Tz.UIForms.FormFields.SelectionType)fAtt.SeletionType;
                    sel.ValueField = fAtt.ValueField;
                    sel.DisplayField = fAtt.DisplayField;
                    sel.DefineSource = fAtt.DefineSource;
                    sel.LookUpSource = fAtt.LookUpSource;
                    sel.ComponentSource = fAtt.ComponentSource;
                    sel.AllowOrderbyAlphabet = fAtt.AllowOrderbyAlphabet;
                    

                    return new JsonpResult(a.AddFormField(formid, sel));                    
                case Tz.UIForms.RenderType.TEXT:
                    Tz.UIForms.FormFields.Text txt = (Tz.UIForms.FormFields.Text)fm;
                    txt.Min = fAtt.Min;
                    txt.Max = fAtt.Max;
                    return new JsonpResult(a.AddFormField(formid, txt));                  
                case Tz.UIForms.RenderType.UPLOAD:
                    Tz.UIForms.FormFields.Upload upd = (Tz.UIForms.FormFields.Upload)fm;
                    upd.MaxFileSize = fAtt.MaxFileSize;
                    upd.FileExtension = fAtt.FileExtension;
                    return new JsonpResult(a.AddFormField(formid, upd));         
            }
            throw new Exception("Invalid Render type");
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