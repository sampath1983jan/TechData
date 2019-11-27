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
        public string CreatedOn { get; set; }
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

        }
        /// <summary>
        /// 
        /// </summary>
        public void LoadComponent() {
            Tz.Data.App.App a = new Data.App.App(Common.GetConnection(this.ClientID));
            DataTable dt = new DataTable();
            dt=a.GetAppComponent(this.ClientID, this.AppID);
            foreach (DataRow r in dt.Rows) {

            }
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
        public bool Remove() {
            Tz.Data.App.App a = new Data.App.App(Common.GetConnection(this.ClientID));
           return a.Remove(this.ClientID, this.AppID);
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
