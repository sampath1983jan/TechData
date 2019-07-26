using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Data;
using Tech.Data.Query;

namespace Tz.Data
{
    public class ImportExport : DataBase
    {
        public ImportExport()
        {
            InitDbs("");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="con"></param>
        /// <param name="folder"></param>
        /// <param name="enableDropDatabase"></param>
        /// <param name="enableCreateDatabase"></param>
        /// <param name="enableDropTable"></param>
        /// <param name="exportTableStructure"></param>
        /// <param name="exportRow"></param>
        /// <param name="enableExportFunction"></param>
        /// <param name="enableExportStoredProcedure"></param>
        /// <param name="enableExportTrigger"></param>
        /// <param name="enableExportEvent"></param>
        /// <param name="enableExportViews"></param>
        /// <param name="maxSqlLength"></param>
        /// <param name="blobExportMode"></param>
        /// <returns></returns>
        public bool Export(string con,
            string folder,
            bool enableDropDatabase,
            bool enableCreateDatabase,
            bool enableDropTable,
            bool exportTableStructure,
            bool exportRow,
            bool enableExportFunction,
            bool enableExportStoredProcedure,
            bool enableExportTrigger,
            bool enableExportEvent,
            bool enableExportViews,
            int maxSqlLength,
            int blobExportMode
            )
        {
            using (MySqlConnection conn = new MySqlConnection(con))
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            mb.ExportInfo.AddDropDatabase = enableDropDatabase;
                            mb.ExportInfo.AddCreateDatabase = enableCreateDatabase;
                            mb.ExportInfo.AddDropTable = enableDropDatabase;
                            mb.ExportInfo.ExportTableStructure = exportTableStructure;
                            mb.ExportInfo.ExportRows = exportRow;
                            mb.ExportInfo.RecordDumpTime = true;
                            mb.ExportInfo.ResetAutoIncrement = true;
                            //mb.ExportInfo.EnableEncryption = cbExEnableEncryption.Checked;
                            //mb.ExportInfo.EncryptionPassword = txtExPassword.Text;
                            mb.ExportInfo.MaxSqlLength = (int)maxSqlLength;
                            mb.ExportInfo.ExportFunctions = enableExportFunction;
                            mb.ExportInfo.ExportProcedures = enableExportStoredProcedure;
                            mb.ExportInfo.ExportTriggers = enableExportTrigger;
                            mb.ExportInfo.ExportEvents = enableExportEvent;
                            mb.ExportInfo.ExportViews = enableExportViews;
                            mb.ExportInfo.ExportRoutinesWithoutDefiner = false;
                            mb.ExportInfo.RowsExportMode = RowsDataExportMode.Insert;
                            mb.ExportInfo.WrapWithinTransaction = true;
                            mb.ExportInfo.TextEncoding = new UTF8Encoding(false);
                            mb.ExportInfo.ExportRoutinesWithoutDefiner = true;
                            // if (dropBlobExportMode.SelectedIndex < 1)
                            if (blobExportMode == 1)
                            {
                                mb.ExportInfo.BlobExportMode = BlobDataExportMode.HexString;
                            }
                            else
                            {
                                mb.ExportInfo.BlobExportMode = BlobDataExportMode.BinaryChar;
                            }
                            mb.ExportInfo.BlobExportModeForBinaryStringAllow = false;
                            mb.ExportToFile(folder);
                            conn.Close();
                        }
                    }
                }
                catch (System.Exception ex) {
                    throw ex;
                }
         
            }
            return true;
        }

        public string Import(string con,
            bool ignoreError,
            string filepath
            ) {


            System.Exception error = null;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(con))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            //mb.ImportInfo.EnableEncryption = cbImEnableEncryption.Checked;
                            //mb.ImportInfo.EncryptionPassword = txtImPwd.Text;
                            mb.ImportInfo.IgnoreSqlError = ignoreError;
                            
                            //mb.ImportInfo.TargetDatabase = txtImTargetDatabase.Text;
                            //mb.ImportInfo.DatabaseDefaultCharSet = txtImDefaultCharSet.Text;
                            mb.ImportInfo.ErrorLogFile = "";

                            mb.ImportFromFile(filepath);

                            error = mb.LastError;
                        }

                        conn.Close();
                    }
                }

                if (error == null)
                    return "done";
                else
                    return error.ToString();
                   // MessageBox.Show("Finished with errors." + Environment.NewLine + Environment.NewLine + error.ToString());
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetExportEventByID(string clientid,string importexportid)
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;

            select = DBQuery.SelectAll(TzAccount.ImportExportEvents.Table).From(TzAccount.ImportExportEvents.Table)
                .WhereField(TzAccount.ImportExportEvents.Table, TzAccount.ImportExportEvents.ClientID.Name, Compare.Equals, DBConst.String(clientid))
                .AndWhere(TzAccount.ImportExportEvents.Table, TzAccount.ImportExportEvents.Status.Name, Compare.Equals, DBConst.Int32(2))
                .AndWhere(TzAccount.ImportExportEvents.Table,TzAccount.ImportExportEvents.ImportExportID.Name, Compare.Equals,DBConst.String(importexportid))
                .OrderBy(TzAccount.ImportExportEvents.EventDateTime.Name, Order.Descending);
            return db.GetDatatable(select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public DataTable GetPendingEvents(string clientid,int status) {
            DBDatabase db;
            db = base.Database;
            DBQuery select;

            select = DBQuery.SelectAll(TzAccount.ImportExportEvents.Table).From(TzAccount.ImportExportEvents.Table)
                .WhereField(TzAccount.ImportExportEvents.Table, TzAccount.ImportExportEvents.ClientID.Name, Compare.Equals, DBConst.String(clientid))
                .AndWhere(TzAccount.ImportExportEvents.Table, TzAccount.ImportExportEvents.Status.Name, Compare.Equals, DBConst.Int32(status))
                .OrderBy(TzAccount.ImportExportEvents.EventDateTime.Name, Order.Descending);
            return db.GetDatatable(select);
        }
        public DataTable GetAllImportEvents(string clientid)
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;

            select = DBQuery.SelectAll(TzAccount.ImportExportEvents.Table).From(TzAccount.ImportExportEvents.Table)
                .WhereField(TzAccount.ImportExportEvents.Table, TzAccount.ImportExportEvents.ClientID.Name, Compare.Equals, DBConst.String(clientid)).
                AndWhere(TzAccount.ImportExportEvents.Table, TzAccount.ImportExportEvents.Type.Name, Compare.Equals, DBConst.Int32(1))
                .OrderBy(TzAccount.ImportExportEvents.EventDateTime.Name, Order.Descending);
            return db.GetDatatable(select);
        }
        public DataTable GetAllExportEvents(string clientid)
        {
            DBDatabase db;
            db = base.Database;
            DBQuery select;

            select = DBQuery.SelectAll(TzAccount.ImportExportEvents.Table).From(TzAccount.ImportExportEvents.Table)
                .WhereField(TzAccount.ImportExportEvents.Table, TzAccount.ImportExportEvents.ClientID.Name, Compare.Equals, DBConst.String(clientid)).
                AndWhere(TzAccount.ImportExportEvents.Table,TzAccount.ImportExportEvents.Type.Name,Compare.Equals,DBConst.Int32(2))
                .OrderBy(TzAccount.ImportExportEvents.EventDateTime.Name, Order.Descending) ;
            return db.GetDatatable(select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="importid"></param>
        /// <param name="status"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool UpdateScheduleStatus(string clientid, string importid,
            int status,string error,string path="")
        {
            DBDatabase db;
            db = base.Database;

            DBConst dbClientID = DBConst.String(clientid);
            DBConst dbImportID = DBConst.String(importid);
            DBConst dbstatus = DBConst.Int32(status);
            DBConst dberror = DBConst.String(error);
            DBConst dbpath = DBConst.String(path);
            DBQuery update = DBQuery.Update(TzAccount.ImportExportEvents.Table).Set(
                TzAccount.ImportExportEvents.Status.Name, dbstatus                
                ).Set(
                TzAccount.ImportExportEvents.Errors.Name, dberror
                ).Set(
                TzAccount.ImportExportEvents.FilePath.Name, dbpath
                ).WhereField(TzAccount.ImportExportEvents.ClientID.Name, Compare.Equals, dbClientID)
                .AndWhere(TzAccount.ImportExportEvents.ImportExportID.Name, Compare.Equals, dbImportID) ;
            int i = db.ExecuteNonQuery(update);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string SaveSchedule(string clientid,
            string exportsettings,
            int status,
            DateTime eventDatetime,
            string filePath,
            bool ignoreError,
            int type
            )
        {
            DBDatabase db;
            db = base.Database;
            string a = Shared.generateID();
            DBConst dbImportExportId = DBConst.String(a);
            DBConst dbclientid = DBConst.String(clientid);
            DBConst dbexportsettings = DBConst.String(exportsettings);
            DBConst dbstatus = DBConst.Int32(status);
            DBConst dbtype = DBConst.Int32(type);
            DBConst dbeventDatetime = DBConst.DateTime(eventDatetime);
            DBConst dbfilePath = DBConst.String(filePath);
            DBConst dbignoreError = DBConst.Const(DbType.Boolean, ignoreError);                  
            DBQuery insert = DBQuery.InsertInto(TzAccount.ImportExportEvents.Table).Fields(
                TzAccount.ImportExportEvents.ClientID.Name,
                TzAccount.ImportExportEvents.ImportExportID.Name,
                TzAccount.ImportExportEvents.ExportSetting.Name,
                TzAccount.ImportExportEvents.Status.Name,
                TzAccount.ImportExportEvents.FilePath.Name,
                TzAccount.ImportExportEvents.IgnoreSQLErrors.Name,
                TzAccount.ImportExportEvents.EventDateTime.Name,
                TzAccount.ImportExportEvents.Type.Name).Values(             
                dbclientid,
                   dbImportExportId,
                dbexportsettings,
                dbstatus,                
                dbfilePath,
                dbignoreError,                
                dbeventDatetime,
                dbtype
                );
            int val = 0;
            using (DbTransaction trans = db.BeginTransaction())
            {
                val = db.ExecuteNonQuery(trans, insert);
                trans.Commit();
            }
            if (val > 0)
            {
                return a;
            }
            else
            {
                return "";
            }

        }
    }
}
