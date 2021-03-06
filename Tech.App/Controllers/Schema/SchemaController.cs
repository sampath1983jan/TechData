﻿using CustomAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tz.Net;
using Tech.App.Models;
using Tz.Net.DataSchema;
using System.Web.Hosting;
using System.IO;
using Tz.ClientManager;

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
        public ActionResult DataScript()
        {
            return View();
        }
        public ActionResult Backup() {
            return PartialView(); 
        }
        public ActionResult Schema() {
            return View();
        }
        public ActionResult Restore() {
            return PartialView();
        }

        #region Schema Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public JsonpResult GetTables(string clientid)
        {
            ClientServer c = new ClientServer(clientid);
            return new JsonpResult(Net.Entity.Table.GetTables(clientid, c.GetServer().ServerID));
        }

        public JsonpResult GetTable(string clientid, string tableid)
        {
ClientServer c = new ClientServer(clientid);
            Tz.Net.DataManager dataManager = new Net.DataManager(tableid, c.GetServer().ServerID, clientid);
            return new JsonpResult(dataManager.GetTable());
        }

        /// <summary>
        ///  new table creation with fields
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="tb"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public JsonpResult SaveTableField(string clientid, string tb, string fields)
        {
            try
            {
                ClientServer c = new ClientServer(clientid);
                Tz.BackApp.Models.Table ModalTable;
                ModalTable = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Table>(tb);
                List<Models.Field> mFields = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Field>>(fields);

                if (ModalTable.TableID == "")
                {
                    Tz.Net.DataManager dataManager = new Net.DataManager(c.GetServer().ServerID, clientid);
                    dataManager.NewTable(ModalTable.TableName, ModalTable.Category);
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
                else
                {
                    Tz.Net.DataManager dataManager = new Net.DataManager(ModalTable.TableID, c.GetServer().ServerID, clientid);
                    if (ModalTable.TableName != "")
                    {
                        if (dataManager.GetTable().TableName != ModalTable.TableName)
                        {
                            dataManager.Rename(ModalTable.TableName, ModalTable.Category); // rename table
                        }
                    }

                    foreach (Models.Field f in mFields)
                    {
                        if (f.IsChanged || f.FieldID != "")
                        {
                            dataManager.ChangeField(f.FieldID, f.FieldName, f.FieldType, f.Length, f.IsNullable, f.IsPrimaryKey); // alter table field info & new field name
                        }
                        else
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
                    }
                    dataManager.AcceptChanges();
                    return new JsonpResult(dataManager.getTableID());
                    //dataManager
                }


            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        ///  change name of the table
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="tb"></param>
        /// <returns></returns>
        public JsonpResult UpdateTable(string clientid, string tb)
        {
            try
            {
                ClientServer c = new ClientServer(clientid);
                Tz.BackApp.Models.Table ModalTable;
                ModalTable = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Table>(tb);
                Tz.Net.DataManager dataManager = new Net.DataManager(ModalTable.TableID, c.GetServer().ServerID, clientid);
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
        public JsonpResult AddField(string clientid, string btid, string fields)
        {
            try
            {
                ClientServer c = new ClientServer(clientid);
                Tz.Net.DataManager dataManager = new Net.DataManager(btid, c.GetServer().ServerID, clientid);
                Models.Field mField = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Field>(fields);
                if (mField.IsPrimaryKey)
                {
                    dataManager.AddPrimarykey(mField.FieldName, mField.FieldType, mField.Length);
                }
                else
                {
                    dataManager.AddField(mField.FieldName, mField.FieldType, mField.Length, mField.IsNullable);
                }
                dataManager.AcceptChanges();
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
        public JsonpResult UpdateField(string clientid, string tbID, string fields)
        {
            try
            {
                ClientServer c = new ClientServer(clientid);
                Tz.Net.DataManager dataManager = new Net.DataManager(tbID, c.GetServer().ServerID, clientid);
                Models.Field mField = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Field>(fields);
                //if (mField.IsPrimaryKey)
                //{
                //   // dataManager.AddPrimarykey(mField.FieldName, mField.FieldType, mField.Length);
                //}
                //else
                //{
                dataManager.ChangeField(mField.FieldID, mField.FieldName, mField.FieldType, mField.Length, mField.IsNullable, mField.IsPrimaryKey, mField.NewFieldName);
                dataManager.AcceptChanges();
                // }
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
ClientServer c = new ClientServer(clientid);
                Tz.Net.DataManager dataManager = new Net.DataManager(tbID, c.GetServer().ServerID, clientid);
                dataManager.Remove();
                return new JsonpResult("true");
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public JsonpResult GetData(int pageSize, int currentPage, string clientid, string tbid)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            ClientServer c = new ClientServer(clientid);
            Tz.Net.DataManager dataManager = new Net.DataManager(tbid, c.GetServer().ServerID, clientid);
            dt = dataManager.GetData(currentPage, pageSize);
            int trecord = dataManager.GetDataCount();

            string dtr = DataResult.Create(dt, pageSize, currentPage, trecord);
            return new JsonpResult(dtr);
        }

        /// <summary>
        /// remove field from the table
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="tbID"></param>
        /// <param name="fieldID"></param>
        /// <returns></returns>
        public JsonpResult RemoveField(string clientid, string tbID, string fieldID)
        {
            try
            {
                ClientServer c = new ClientServer(clientid);
                Tz.Net.DataManager dataManager = new Net.DataManager(tbID, c.GetServer().ServerID, clientid);
                dataManager.RemoveField(fieldID);
                return new JsonpResult("true");
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public Task syncSchema(string clientid)
        {
            try
            {
                return Task.Run(() =>
                {
                    Net.DataManager.synSever(clientid);
                });
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public JsonpResult GetExportHistory(string clientid) {
            ImportExport im = new ImportExport(clientid);
            return new JsonpResult(im.GetExportList());
        }
        [HttpPost]
        public JsonpResult Upload(FormCollection collection) {
            string _uploadFolder = "~/tempfiles";
            string cn = collection["ChunkNumber"];
            Int64 cz =  Convert.ToInt64(collection["ChunkSize"]);
            Int64 tz = Convert.ToInt64(collection["totalSize"]);
            string unique = collection["unique"];
            string FileName = collection["FileName"];
            string uploadingProcess = collection["uploadingProcess"];
           // string file = collection["file"];

            string filePath;
            filePath = string.Format("{0}/{1}/{1}.part{2}", _uploadFolder, unique, System.Convert.ToInt32(cn).ToString("0000"));
            string localFilePath = Request.RequestContext.HttpContext.Server.MapPath(filePath);
            string directory = System.IO.Path.GetDirectoryName(localFilePath);
            if(!System.IO.Directory.Exists(localFilePath)) {
                System.IO.Directory.CreateDirectory(directory);
            }
            if (!System.IO.File.Exists(localFilePath)) {
                Request.Files[0].SaveAs(localFilePath);
            }
            IOrderedEnumerable<System.IO.FileInfo> filePaths = new DirectoryInfo(directory).GetFiles()
.OrderBy(f => f.CreationTime);
            var files = System.IO.Directory.GetFiles(directory);
            if((files.Length) * Convert.ToInt64(cz) >= tz) {
                filePath = string.Format("{0}/{1}{2}", _uploadFolder, unique, System.IO.Path.GetExtension(HttpContext.Request.Params["FileName"]));
                localFilePath = Request.RequestContext.HttpContext.Server.MapPath(filePath);

                var fs = new FileStream(localFilePath, FileMode.CreateNew);
                foreach (FileInfo file in filePaths)
                {
                    var buffer = System.IO.File.ReadAllBytes(file.FullName);
                    fs.Write(buffer, 0, buffer.Length);
                    System.IO.File.Delete(file.FullName);
                }
                fs.Close();
                fs.Dispose();                
                System.IO.Directory.Delete(directory);
                //filePath

            }         


            return new JsonpResult(filePath);

        }
        public ActionResult Download(string clientid, string historyid)
        {

            string filePath = @"D:/temp"; //Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["FileManagementPath"]);
            ImportExport im = new ImportExport(clientid);
            ExportEvent ev= im.getExportEvent(historyid);

            string actualFilePath = filePath + "" + ev.FolderPath;
            string filename = Path.GetFileName(actualFilePath);
            byte[] fileBytes = System.IO.File.ReadAllBytes(actualFilePath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);

            
        }
        public Task Export(string clientid,string exportSetting) {
            try
            {              

                return Task.Run(() =>
                {
                    HostingEnvironment.QueueBackgroundWorkItem(cancellationToken => new Models.Worker().StartProcessing(cancellationToken, clientid
                        ,Newtonsoft.Json.JsonConvert.DeserializeObject<ExportSettings>(exportSetting)));
                });
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public Task Import(string clientid, string path,bool ignore)
        {
            //string _uploadFolder = "~/tempfiles";
            //path = _uploadFolder + "/" + path;
            try
            {
                return Task.Run(() =>
                {
                    HostingEnvironment.QueueBackgroundWorkItem(cancellationToken => new Models.Worker().StartImportProcess(cancellationToken, clientid
                        ,ignore,path));
                });
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DataScript
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scriptname"></param>
        /// <param name="category"></param>
        /// <param name="script"></param>
        /// <returns></returns>
        public JsonpResult SaveDataScript(string scriptname, string category, string script,string intend) {
            DataScript ds = new DataScript(scriptname,category,script);
            ds.AddIntend(intend);
            ds.Save();
            //if (ds.ScriptID != "") {
            //    ScriptIntend sc = new ScriptIntend(ds.ScriptID, intend);
            //    sc.Save();
            //}
            return new JsonpResult(ds.ScriptID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scriptname"></param>
        /// <param name="category"></param>
        /// <param name="script"></param>
        /// <param name="scriptid"></param>
        /// <returns></returns>
        public JsonpResult UpdateDataScript(string scriptname, string category, string script, string scriptid, string intend) {
            DataScript ds = new DataScript(scriptid);
            ds.Script = script;
            ds.Name = scriptname;
            ds.Category = category;
            ds.AddIntend(intend);
            ds.Save();
            //ScriptIntend sc = new ScriptIntend(ds.ScriptID, intend);
            //sc.Update();
            return new JsonpResult(ds.ScriptID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scriptID"></param>
        /// <returns></returns>
        public JsonpResult RemoveDataScript(string scriptID) {
            DataScript ds = new DataScript(scriptID);
            var rs =ds.Remove();
            //if (rs == true) {
            //    ScriptIntend sc = new ScriptIntend(ds.ScriptID);
            //    sc.Remove();
            //}
            return new JsonpResult( rs);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonpResult GetDataScripts() {
            return new JsonpResult(Tz.Net.DataScript.GetDataScripts());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scriptid"></param>
        /// <returns></returns>
        public JsonpResult GetDataScript(string scriptid) {
            DataScript ds = new DataScript(scriptid);
            return new JsonpResult(ds);
        }
        #endregion

    }
}
