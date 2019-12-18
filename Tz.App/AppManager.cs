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

namespace Tz.App
{
    public enum AppElementType {
        PAGE=0,
        COMPONENT = 1,
        DASHBOARD=2,
        REPORT=3,
        FORM=4,
        FEATURE=5,
        ANALYTIC=6
    }
    public class AppManager
    {
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
        public List<AppElement.AppForm> Forms { get; }
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
           // var clist = new List<IComponent>();
            //dtFields = dt.DefaultView.ToTable(true, "FieldID",
            //    "AttributeName",
            //    "ComponentID",
            //    "IsRequired",
            //    "IsUnique",
            //    "IsCore",
            //    "IsReadOnly",
            //    "IsSecured",
            //    "IsAuto",
            //    "DefaultValue",
            //    "FileExtension",
            //    "RegExp",
            //    "AttributeType"
            //    );
            foreach (DataRow dr in dttable.Rows)
            {             
               // var c = new Tz.Core. Component(this.ClientID);
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
                //foreach (DataRow drow in dtFields.Rows)
                //{
                //    string _fieldid = "";
                //    _fieldid = drow.IsNull("FieldID") ? "" : drow["FieldID"].ToString();
                //    Core.ComponentAttribute ca = new Core.ComponentAttribute(this.ClientID, c.TableID, _fieldid);
                //    ca.AttributeName = drow.IsNull("AttributeName") ? "" : drow["AttributeName"].ToString();
                //    // ca.FieldName = drow.IsNull("FieldName") ? "" : drow["FieldName"].ToString();
                //    ca.IsRequired = drow.IsNull("IsRequired") ? false : Convert.ToBoolean(drow["IsRequired"]);
                //    ca.IsUnique = drow.IsNull("IsUnique") ? false : Convert.ToBoolean(drow["IsUnique"]);
                //    ca.IsCore = drow.IsNull("IsCore") ? false : Convert.ToBoolean(drow["IsCore"]);
                //    ca.IsReadOnly = drow.IsNull("IsReadOnly") ? false : Convert.ToBoolean(drow["IsReadOnly"]);
                //    ca.IsSecured = drow.IsNull("IsSecured") ? false : Convert.ToBoolean(drow["IsSecured"]);
                //    ca.IsAuto = drow.IsNull("IsAuto") ? false : Convert.ToBoolean(drow["IsAuto"]);
                //    ca.DefaultValue = drow.IsNull("DefaultValue") ? "" : drow["DefaultValue"].ToString();
                //    ca.FileExtension = drow.IsNull("FileExtension") ? "" : drow["FileExtension"].ToString();
                //    ca.RegExp = drow.IsNull("RegExp") ? "" : drow["RegExp"].ToString();
                //    ca.AttributeType = drow.IsNull("AttributeType") ? Core.ComponentAttribute.ComoponentAttributeType._string : (Core.ComponentAttribute.ComoponentAttributeType)drow["AttributeType"];
                //                     c.Attributes.Add(ca);
                //}
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
            foreach (DataRow r in dt.Rows)
            {

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctype"></param>
        /// <returns></returns>
        public Tz.Core.IComponent NewComponent(ComponentType ctype) {
            return new Tz.Core.ComponentManager(ctype, this.ClientID, "", "", "", "").Component ;
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
        public bool UpdateComponent(Tz.Core.IComponent c) {
            var cc = new AppElement.AppComponent(this.ClientID, this.AppID, c.ComponentID);
            cc.Load();
            cc.Component.ComponentName = c.ComponentName;
            cc.Component.Category = c.Category;
            cc.Component.Title = c.Title;
            cc.Component.TitleFormat = c.TitleFormat;
          
            var a = new Tz.Core.ComponentManager(cc.Component);
            return a.UpdateComponent();


        }
        public bool RemoveComponent(string componentID) {
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
        public bool PublishComponent(string componentID) {
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
            catch (Exception ex) {
                throw new Exception(ex.Message, ex.InnerException);
            }
          
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ElementID"></param>
        /// <returns></returns>
        public IComponent GetComponent(string ElementID) {
          var c=   this.Components.Where(x => x.Component.ComponentID == ElementID).FirstOrDefault();
            if (c == null)
            {
                var aa = new ComponentManager(this.ClientID, ElementID);
                return aa.Component;
            }
            else
                return null;      
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Save() {
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
            else {
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
