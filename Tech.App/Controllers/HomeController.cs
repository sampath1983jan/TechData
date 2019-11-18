using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Tech.Data.Schema;
using Tech.Data.Query;
using Tech.App.Models;
using Tz.ClientManager;
namespace Tech.App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Client()
        {
            ViewBag.Message = "Your Client page.";
            return View();
        }        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="currentPage"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public JsonResult GetData(string clientid,string tb,int currentPage,int PageSize)
        {
            ClientServer c = new ClientServer(clientid);

            System.Data.DataTable dt = new DataTable();
            Data.DBDatabase db = Data.DBDatabase.Create(c.GetServer().Connection(), "MySql.Data.MySqlClient");
            // Data.DBDatabase db = Data.DBDatabase.Create("Server=52.163.241.42; Uid=admin;Pwd=smrtalentoz3106;Initial Catalog=jumbo_talentoz; ", "MySql.Data.MySqlClient");
            //Data.Schema.DBSchemaProvider provider = db.GetSchemaProvider();
            //DBSchemaTable tables = provider.GetTable(tb);
            string dbname = c.GetServer().DBName;
            DBQuery totalRecord = DBQuery.SelectCount().From(dbname, tb);
            int trecord = Convert.ToInt32(db.ExecuteScalar(totalRecord));
            DBQuery record = DBQuery.SelectAll().From(dbname, tb).TopRange(currentPage* PageSize, PageSize);
            var dtRecord= db.GetDatatable(record);
            string dtr =   DataResult.Create(dtRecord, PageSize, currentPage, trecord);

            //tables.Columns 

           return Json(dtr, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetEntity(string clientid) {
            System.Data.DataTable dt = new DataTable();
            ClientServer c = new ClientServer(clientid);
            Data.DBDatabase db = Data.DBDatabase.Create(c.GetServer().Connection(), "MySql.Data.MySqlClient");
          // Data.DBDatabase db = Data.DBDatabase.Create("Server=52.163.241.42; Uid=admin;Pwd=smrtalentoz3106;Initial Catalog=jumbo_talentoz; ", "MySql.Data.MySqlClient");
            //Server=52.163.241.42; Uid=admin;Pwd=smrtalentoz3106;Initial Catalog=jumbo_talentoz; 
            Data.Schema.DBSchemaProvider provider = db.GetSchemaProvider();
            IEnumerable<Data.Schema.DBSchemaItemRef> tables = provider.GetAllTables();
            IEnumerable<Data.Schema.DBSchemaItemRef> tables1 = provider.GetAllViews() ;

            tables= tables.Concat(tables1);
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(tables), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        public JsonResult GetFields(string clientid, string tb) {
            System.Data.DataTable dt = new DataTable();
            ClientServer c = new ClientServer(clientid);
            Data.DBDatabase db = Data.DBDatabase.Create(c.GetServer().Connection(), "MySql.Data.MySqlClient");
          // Data.DBDatabase db = Data.DBDatabase.Create("Server=52.163.241.42; Uid=admin;Pwd=smrtalentoz3106;Initial Catalog=jumbo_talentoz; ", "MySql.Data.MySqlClient");
            Data.Schema.DBSchemaProvider provider = db.GetSchemaProvider();
            DBSchemaTable tables = provider.GetTable(tb);
            if (tables == null)
            {
                DBSchemaView view = provider.GetView(tb);
                return Json(Newtonsoft.Json.JsonConvert.SerializeObject(view), JsonRequestBehavior.AllowGet);
            }
            else {

                return Json(Newtonsoft.Json.JsonConvert.SerializeObject(tables), JsonRequestBehavior.AllowGet);
            }
            
            
        }
    }
}