using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tz.Global.TzAccount;
using Tz.Core;
using Tz.Data;
using Tz.Global;
using Tz.Security;
using Tz.Page;
using Tz.Report;
using Tz.Dashboard;
using System.Data;
using Tz.UIForms;

namespace Tz.App
{
    public enum AppElementType {
        PAGE=0,
        COMPONENT = 1,
        DASHBOARD=2,
        REPORT=3,
        FORM=4,
        FEATURE=5,
        ANALYTIC=6,
        LOOKUP=7,
            
    }
    public class AppManager
    {
        private List<AppElement.AppForm> _forms;
        private List<AppElement.AppLookup> _lookups;
        private string _appid;
        /// <summary>
        /// 
        /// </summary>
        public string ClientID { get;}
        /// <summary>
        /// 
        /// </summary>
        public string AppID { get { return _appid; }  }        
        /// <summary>
        /// 
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TimeZone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int DateFormat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TimeFormat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<App.AppElement.AppComponent> Components { get; }
        /// <summary>
        /// 
        /// </summary>
        public List<AppElement.AppWebPage> Pages { get;  }
        /// <summary>
        /// 
        /// </summary>
        public List<AppElement.AppReport> Reports { get; }
        /// <summary>
        /// 
        /// </summary>
        public List<AppElement.AppDashboard> Dashboards { get; }
        /// <summary>
        /// 
        /// </summary>
        public List<AppElement.AppLookup> Lookups { get { return _lookups; } }
        /// <summary>
        /// 
        /// </summary>
        public List<AppElement.AppForm> Forms { get { return _forms; } }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        public AppManager(string clientid) {
            this.ClientID = clientid;
            _appid = "";
            this.Description = "";
            this.DateFormat = 0;
            this.TimeFormat = 0;
            this.TimeZone = 0;
            this.Category = "None";
            this.AppName = "";
            Components = new List<AppElement.AppComponent>();
            _forms = new List<AppElement.AppForm>();
            Pages = new List<AppElement.AppWebPage>();
            Reports = new List<AppElement.AppReport>();
            Dashboards = new List<AppElement.AppDashboard>();
            _lookups = new List<AppElement.AppLookup>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="appid"></param>
        public AppManager(string clientid,
            string appid)
        {
            this.ClientID = clientid;
            _appid = appid;
            Components = new List<AppElement.AppComponent>();
            _forms = new List<AppElement.AppForm>();
            Pages = new List<AppElement.AppWebPage>();
            Reports = new List<AppElement.AppReport>();
            Dashboards = new List<AppElement.AppDashboard>();
            _lookups = new List<AppElement.AppLookup>();
            Load();
        }
        private void Load()
        {
            Tz.Data.App.App a = new Data.App.App(Common.GetConnection(this.ClientID));
            var dt = new DataTable();
            dt = a.GetApp(this.ClientID,this.AppID);           
            foreach (DataRow dr in dt.Rows)
            {                
              //  am._appid = dr["AppID"] == null ? "" : (string)dr["AppID"];
                this.AppName = dr["Name"] == null ? "" : (string)dr["Name"];
                this.Description = dr["Description"] == null ? "" : (string)dr["Description"];
                this.CreatedOn = DBNull.Value.Equals(dr["CreatedOn"]) == true ? DateTime.Now : (DateTime)dr["CreatedOn"];
                this.Category = dr["Category"] == null ? "" : (string)dr["Category"];
                this.TimeZone = DBNull.Value.Equals(dr["TimeZone"]) == true ? 0 : (Int32)dr["TimeZone"];
                this.DateFormat = DBNull.Value.Equals(dr["DateFormat"]) == true ? 0 : (Int32)dr["DateFormat"];
                this.TimeFormat = DBNull.Value.Equals(dr["TimeFormat"]) == true ? 0 : (Int32)dr["TimeFormat"];           
            }           
        }
        public static List<AppManager> GetAppManagers(string clientid) {
            Tz.Data.App.App a = new Data.App.App(Common.GetConnection(clientid));
            var dt = new DataTable();
              dt=  a.GetApps(clientid);
            var ams = new List<AppManager>();
            foreach (DataRow dr in dt.Rows) {
                var am = new AppManager(clientid);
                am._appid = dr["AppID"] == null  ? "" : (string)dr["AppID"];
                am.AppName = dr["Name"] == null ? "" : (string)dr["Name"];
                am.Description = dr["Description"] == null ? "" : (string)dr["Description"];
                am.CreatedOn = DBNull.Value.Equals(dr["CreatedOn"]) == true ? DateTime.Now : (DateTime)dr["CreatedOn"];
                am.Category = dr["Category"] == null ? "" : (string)dr["Category"];
                am.TimeZone = DBNull.Value.Equals(dr["TimeZone"]) ==true ? 0 : (Int32)dr["TimeZone"];
                am.DateFormat = DBNull.Value.Equals(dr["DateFormat"]) == true ? 0 : (Int32)dr["DateFormat"];
                am.TimeFormat = DBNull.Value.Equals(dr["TimeFormat"]) == true ? 0 : (Int32)dr["TimeFormat"];
                ams.Add(am);
            }
            return ams;
        }
        /// <summary>
        /// 
        /// </summary>
        public List<App.AppElement.AppComponent> GetComponents() {
            var acs= new List< App.AppElement.AppComponent>();
            Tz.Data.App.App a = new Data.App.App(Common.GetConnection(this.ClientID));
            DataTable dt, dtFields = new DataTable();
            dt=a.GetAppComponent(this.ClientID, this.AppID);
            var dttable = dt.DefaultView.ToTable(true,
                 "ComponentID",
                 "ComponentName",
                 "ComponentType",
                 "Title",
                 "TableID",
                 "PrimaryKeys",
                 "TitleFormat",
                 "Category",
                 "IsGlobal"
                 );            
            foreach (DataRow dr in dttable.Rows)
            {           
                string cid = dr.IsNull("ComponentID") ? "" : dr["ComponentID"].ToString();
                var ac = new App.AppElement.AppComponent(this.ClientID, this.AppID, cid);
                var c = ac.Component;
                c.ClientID = this.ClientID;
                c.ComponentID = cid;
                c.ComponentName = dr.IsNull("ComponentName") ? "" : dr["ComponentName"].ToString();
                c.ComponentType = dr.IsNull("ComponentType") ? Tz.Core.ComponentType.none : (Tz.Core.ComponentType)dr["ComponentType"];
                c.Title = dr.IsNull("Title") ? "" : dr["Title"].ToString();
                c.TableID = dr.IsNull("TableID") ? "" : dr["TableID"].ToString();
                c.PrimaryKeys = dr.IsNull("PrimaryKeys") ? "" : dr["PrimaryKeys"].ToString();
                c.TitleFormat = dr.IsNull("TitleFormat") ? "" : dr["TitleFormat"].ToString();
                c.Category = dr.IsNull("Category") ? "" : dr["Category"].ToString();
                c.IsGlobal = dr.IsNull("IsGlobal") ? false : Convert.ToBoolean(dr["IsGlobal"]);              
                this.Components.Add(ac);
                acs.Add(ac);
            }
            return acs;
        }
        /// <summary>
        /// 
        /// </summary>
        public void LoadPages() {
            Tz.Data.App.App a = new Data.App.App(Common.GetConnection(this.ClientID));
            DataTable dt = new DataTable();
            dt = a.GetAppPage(this.ClientID, this.AppID);
            foreach (DataRow r in dt.Rows)
            {

            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void LoadForms()
        {
            Tz.Data.App.App a = new Data.App.App(Common.GetConnection(this.ClientID));
            DataTable dt = new DataTable();
            dt = a.GetAppForm(this.ClientID, this.AppID);
            string s = string.Join(", ", dt.Rows.OfType<DataRow>().Select(r => r["ElementID"].ToString()));
         var uforms =   Tz.UIForms.Form.GetForms(this.ClientID,s);
                      foreach (UIForms.Form f in uforms) {
                var apForm = new App.AppElement.AppForm(this.ClientID, this.AppID);
                apForm.Set(f);
                this.Forms.Add(apForm);
            }          
        }
        public void LoadLookUp() {
            Tz.Data.App.App a = new Data.App.App(Common.GetConnection(this.ClientID));
            DataTable dt = new DataTable();
            dt = a.GetAppLookups(this.ClientID, this.AppID);
            string s = string.Join(", ", dt.Rows.OfType<DataRow>().Select(r => r["ElementID"].ToString()));
           var lookups= Tz.Core.Lookup.GetLookUps(this.ClientID, s);
            foreach (Tz.Core.Lookup l in lookups) {
                var apLookup = new App.AppElement.AppLookup(this.ClientID, this.AppID);              
                apLookup.Set(l);
                this.Lookups.Add(apLookup);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void LoadReports()
        {
            Tz.Data.App.App a = new Data.App.App(Common.GetConnection(this.ClientID));
            DataTable dt = new DataTable();
            dt = a.GetAppReport(this.ClientID, this.AppID);
            foreach (DataRow r in dt.Rows)
            {

            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void LoadDashboard()
        {
            Tz.Data.App.App a = new Data.App.App(Common.GetConnection(this.ClientID));
            DataTable dt = new DataTable();
            dt = a.GetAppDashboard(this.ClientID, this.AppID);
            foreach (DataRow r in dt.Rows)
            {

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Remove() {
            Tz.Data.App.App a = new Data.App.App(Common.GetConnection(this.ClientID));
           return a.Remove(this.ClientID, this.AppID);
        }
        #region Component
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctype"></param>
        /// <returns></returns>
        public Tz.Core.IComponent NewComponent(ComponentType ctype)
        {
            return new Tz.Core.ComponentManager(ctype, this.ClientID, "", "", "", "").Component;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool SaveComponent(Tz.Core.IComponent c)
        {
            var a = new Tz.Core.ComponentManager(c);
            string compID = a.SaveComponent();
            if (compID != "")
            {
                var cc = new AppElement.AppComponent(this.ClientID, this.AppID, compID);
                this.Components.Add(cc);
                if (cc.Assign()) { return true; }
                else return false;
            }
            else return false;
        }
        public bool UpdateComponent(Tz.Core.IComponent c)
        {
            var cc = new AppElement.AppComponent(this.ClientID, this.AppID, c.ComponentID);
            cc.Load();
            cc.Component.ComponentName = c.ComponentName;
            cc.Component.Category = c.Category;
            cc.Component.Title = c.Title;
            cc.Component.TitleFormat = c.TitleFormat;

            var a = new Tz.Core.ComponentManager(cc.Component);
            return a.UpdateComponent();


        }
        public bool RemoveComponent(string componentID)
        {
            if (this.Components.Count == 0)
                GetComponents();
            var comp = this.GetComponent(componentID);
            Tz.Data.App.App a = new Data.App.App(Common.GetConnection(this.ClientID));
            Core.ComponentManager mg = new ComponentManager(comp);
            if (mg.Remove())
            {
                a.RemoveAppComponent(this.ClientID, this.AppID, componentID);
                return true;
            }
            else
                return false;
        }
        public bool PublishComponent(string componentID)
        {
            if (this.Components.Count == 0)
                GetComponents();
            var comp = this.GetComponent(componentID);
            comp.LoadAttributes();
            Tz.Data.App.App a = new Data.App.App(Common.GetConnection(this.ClientID));
            Core.ComponentManager mg = new ComponentManager(comp);
            try
            {
                if (mg.Publish() != "")
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ElementID"></param>
        /// <returns></returns>
        public IComponent GetComponent(string ElementID)
        {
            var c = this.Components.Where(x => x.Component.ComponentID == ElementID).FirstOrDefault();
            if (c != null)
            {
                var aa = new ComponentManager(this.ClientID, ElementID);
                return aa.Component;
            }
            else
                return null;
        }

        public Tz.Core.Lookup GetLookUp(string ElementID) {
            var l = this.Lookups.Where(x => x.Lookup.LookupID == ElementID).FirstOrDefault();
            if (l != null)
            {
                l.Lookup.Load();
                return l.Lookup;
            }
            else
                return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            Tz.Data.App.App a = new Data.App.App(Common.GetConnection(this.ClientID));
            if (this.AppID == "")
            {
                _appid = a.Save(this.ClientID,
                this.AppName,
                this.Description,
                this.Category
                );
                if (_appid != "")
                {
                    return true;
                }
                else { return false; }
            }
            else
            {
                if (a.Update(this.ClientID,
                     this.AppID,
                     this.AppName,
                     this.Description,
                     this.Category))
                {
                    return true;
                }
                else
                    return false;
            }

        }

        #endregion
        #region Form
        public UIForms.Form NewForm() {                
            var k = new List<UIFormKey>();
         var   formBuilder = new FormBuilder(this.ClientID,"", k, new FormFieldBuilder());
            return   formBuilder.UIForm;            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formid"></param>
        /// <returns></returns>
        public UIForms.Form GetForm(string formid) {
            var appform = this.Forms.Where(x => x.Form.FormID == formid).FirstOrDefault();
            return appform.Form;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public bool SaveForm(UIForms.Form f)
        {
            if (f.Save())
            {
                var cc = new AppElement.AppForm(this.ClientID, this.AppID, f.FormID);
                this.Forms.Add(cc);
                if (cc.Assign()) { return true; }
                else
                    return false;
            }
            else return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formid"></param>
        /// <returns></returns>
        public bool RemoveForm(string formid) {
            var appform = this.Forms.Where(x => x.Form.FormID == formid).FirstOrDefault();
            var fb = new FormBuilder(appform.Form, new FormFieldBuilder());
            if (fb.Remove())
            {
                appform.Remove();
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formid"></param>
        /// <param name="renderType"></param>
        /// <returns></returns>
        public FormField NewField(string formid,RenderType renderType,string attID) {
          var appform=  this.Forms.Where(x => x.Form.FormID == formid).FirstOrDefault();
            Core.Component c = new Core.Component(this.ClientID, appform.Form.ComponentID);
            c.LoadAttributes();
           var att =c.Attributes.Where (x => x.FieldID == attID).FirstOrDefault();            
            var fb = new FormBuilder(appform.Form, new FormFieldBuilder());
            var f = fb.NewField(renderType);
            f.Attribute.FieldAttribute = att;
            return f;        
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formid"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public bool AddFormField(string formid, FormField field) {
            var appform = this.Forms.Where(x => x.Form.FormID == formid).FirstOrDefault();

            var fb = new FormBuilder(appform.Form, new FormFieldBuilder());
            return fb.SaveField(field);
        }
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formid"></param>
        /// <param name="fieldid"></param>
        /// <returns></returns>
        public bool RemoveField(string formid, string fieldid) {
            var appform = this.Forms.Where(x => x.Form.FormID == formid).FirstOrDefault();
            return appform.Form.RemoveField(fieldid);
        }
        #endregion
        #region Lookup
        public Tz.Core.Lookup NewLookup() {
            Tz.Core.Lookup lk = new Lookup(this.ClientID);
            return lk;           
            
        }
        public bool SaveLookup(Tz.Core.Lookup lk) {
            if (lk.Save())
            {
                var cc = new AppElement.AppLookup(this.ClientID, this.AppID, lk.LookupID);
                this.Lookups.Add(cc);
                if (cc.Assign()) { return true; }
                else
                    return false;
            }
            else
                return false;
        }
        public bool RemoveLookUp(string lkid) {
            var lk = this.Lookups.Where(x => x.Lookup.LookupID == lkid).FirstOrDefault();
            if (lk.Lookup.Remove())
            {
                lk.Remove();
                return true;
            }
            else
                return false;
        }



        #endregion
    }

    public static class Common
    {
        public static string GetConnection(string clientID)
        {
            string conn;
            Tz.ClientManager.ClientServer ck;
            ck = new Tz.ClientManager.ClientServer(clientID);
            conn = ck.GetServer().Connection();
            return conn;
        }
    }
}
