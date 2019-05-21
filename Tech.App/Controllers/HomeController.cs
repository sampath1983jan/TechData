using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Tech.Data.Schema;
using Tech.Data.Query;
using Tech.App.Models;

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


        public JsonResult GetData(string tb,int currentPage,int PageSize)
        {
            System.Data.DataTable dt = new DataTable();
            Data.DBDatabase db = Data.DBDatabase.Create("Server=dell6;Initial Catalog=talentozdev;Uid=root;Pwd=admin312", "MySql.Data.MySqlClient");
            // Data.DBDatabase db = Data.DBDatabase.Create("Server=52.163.241.42; Uid=admin;Pwd=smrtalentoz3106;Initial Catalog=jumbo_talentoz; ", "MySql.Data.MySqlClient");
            //Data.Schema.DBSchemaProvider provider = db.GetSchemaProvider();
            //DBSchemaTable tables = provider.GetTable(tb);
            DBQuery totalRecord = DBQuery.SelectCount().From("talentozdev",tb);
            int trecord = Convert.ToInt32(db.ExecuteScalar(totalRecord));
            DBQuery record = DBQuery.SelectAll().From("talentozdev", tb).TopRange(currentPage* PageSize, PageSize);
            var dtRecord= db.GetDatatable(record);
            string dtr =   DataResult.Create(dtRecord, PageSize, currentPage, trecord);

            //tables.Columns 

           return Json(dtr, JsonRequestBehavior.AllowGet);
        }
            public JsonResult GetEntity() {
            System.Data.DataTable dt = new DataTable();
            Data.DBDatabase db = Data.DBDatabase.Create("Server=dell6;Initial Catalog=talentozdev;Uid=root;Pwd=admin312", "MySql.Data.MySqlClient");
          // Data.DBDatabase db = Data.DBDatabase.Create("Server=52.163.241.42; Uid=admin;Pwd=smrtalentoz3106;Initial Catalog=jumbo_talentoz; ", "MySql.Data.MySqlClient");
            //Server=52.163.241.42; Uid=admin;Pwd=smrtalentoz3106;Initial Catalog=jumbo_talentoz; 
            Data.Schema.DBSchemaProvider provider = db.GetSchemaProvider();
            IEnumerable<Data.Schema.DBSchemaItemRef> tables = provider.GetAllTables();
            IEnumerable<Data.Schema.DBSchemaItemRef> tables1 = provider.GetAllViews() ;

            tables= tables.Concat(tables1);
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(tables), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFields(string tb) {
            System.Data.DataTable dt = new DataTable();
            Data.DBDatabase db = Data.DBDatabase.Create("Server=dell6;Initial Catalog=talentozdev;Uid=root;Pwd=admin312", "MySql.Data.MySqlClient");
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