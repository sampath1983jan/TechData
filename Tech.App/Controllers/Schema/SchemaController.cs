using CustomAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tz.BackApp.Controllers.Schema
{
    [CustomAuthorize(Roles = "Admin")]
    public class SchemaController : Controller
    {
        // GET: Schema
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Schema() {
            return View();
        }

        /// <summary>
        ///  new table creation with fields
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="tb"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public JsonpResult SaveTableField(string clientid,string tb,string fields) {
            try
            {
                Tz.Net.ClientServer c = new Net.ClientServer(clientid);
                Tz.BackApp.Models.Table ModalTable;
                ModalTable = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Table>(tb);
                List<Models.Field> mFields = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Field>>(fields);
                Tz.Net.DataManager dataManager = new Net.DataManager(c.GetServer().ServerID);
                dataManager.NewTable( ModalTable.TableName, ModalTable.Category);
                foreach (Models.Field f in mFields)
                {
                    if (f.IsPrimaryKey)
                    {
                        dataManager.AddPrimarykey(f.FieldName, f.FieldType, f.Length);
                    }
                    else
                    {
                        dataManager.AddField(f.FieldName, f.FieldType, f.Length, f.IsNullable);
                    }
                }
                dataManager.AcceptChanges();
                return new JsonpResult(dataManager.getTableID());
            }
            catch (System.Exception ex) {
                throw ex;
            }
            
        }
        /// <summary>
        ///  change name of the table
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="tb"></param>
        /// <returns></returns>
        public JsonpResult UpdateTable(string clientid,string tb) {
            try { 
            Tz.Net.ClientServer c = new Net.ClientServer(clientid);
            Tz.BackApp.Models.Table ModalTable;
            ModalTable = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Table>(tb);
            Tz.Net.DataManager dataManager = new Net.DataManager(ModalTable.TableID,c.GetServer().ServerID);
            dataManager.Rename(ModalTable.TableName, ModalTable.Category);
                return new JsonpResult(dataManager.getTableID());
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            

        }
        /// <summary>
        ///  add new field in the existing table
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="btid"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public JsonpResult AddField(string clientid, string btid, string fields) {
            try { 
            Tz.Net.ClientServer c = new Net.ClientServer(clientid);
            Tz.Net.DataManager dataManager = new Net.DataManager(btid, c.GetServer().ServerID);
            Models.Field mField = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Field>(fields);
            if (mField.IsPrimaryKey)
            {
                dataManager.AddPrimarykey(mField.FieldName, mField.FieldType, mField.Length);
            }
            else {
                dataManager.AddField(mField.FieldName, mField.FieldType, mField.Length, mField.IsNullable);
                }
                return new JsonpResult(dataManager.getTableID());
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// update existing field
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="tbID"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public JsonpResult UpdateField(string clientid,string tbID, string fields) {
            try
            {
                Tz.Net.ClientServer c = new Net.ClientServer(clientid);
                Tz.Net.DataManager dataManager = new Net.DataManager(tbID, c.GetServer().ServerID);
                Models.Field mField = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Field>(fields);
                if (mField.IsPrimaryKey)
                {
                   // dataManager.AddPrimarykey(mField.FieldName, mField.FieldType, mField.Length);
                }
                else
                {
                    dataManager.ChangeField(mField.FieldID, mField.FieldName, mField.FieldType, mField.Length, mField.IsNullable, mField.NewFieldName);
                }
                return new JsonpResult(dataManager.getTableID());
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// remove table
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="tbID"></param>
        /// <returns></returns>
        public JsonpResult RemoveTable(string clientid, string tbID)
        {
            try
            {
                Tz.Net.ClientServer c = new Net.ClientServer(clientid);
                Tz.Net.DataManager dataManager = new Net.DataManager(tbID, c.GetServer().ServerID);
                dataManager.Remove();
                return new JsonpResult("true");
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// remove field from the table
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="tbID"></param>
        /// <param name="fieldID"></param>
        /// <returns></returns>
        public JsonpResult RemoveField(string clientid, string tbID, string fieldID) {
            try
            {
                Tz.Net.ClientServer c = new Net.ClientServer(clientid);
                Tz.Net.DataManager dataManager = new Net.DataManager(tbID, c.GetServer().ServerID);
                dataManager.RemoveField(fieldID);
                return new JsonpResult("true");
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}