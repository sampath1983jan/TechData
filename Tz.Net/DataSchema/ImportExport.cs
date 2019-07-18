using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Tz.Net.DataSchema
{
    public enum ImportExportStatus {
        pending,
        progress,
        completed,
        error
    }
    public class ImportExport
    {
        public string ClientID { get; set; }              
        public string ExportFolderPath { get; set; }             
        public ImportExport(string clientid)
        {
            this.ClientID = clientid;                        
        }
        public ExportEvent ExportScheduleNow(ExportSettings Export,string folderPath) {
            ExportEvent exp = new ExportEvent();
            exp.ClientID = this.ClientID;
            exp.ExportDate = DateTime.Now;
            exp.Settings = Export;
            exp.FolderPath = folderPath;
            exp.Save();
            return exp;
        }

        public ImportEvent ImportScheduleNow(bool ignorError, string filepath) {
            ImportEvent imp = new ImportEvent();
            imp.ClientID = this.ClientID;
            imp.IgnoreSQLErrors = ignorError;
            imp.ImportFilePath = filepath;
             imp.Save();
            return imp;
        }

        public ExportEvent ExportScheduleDate(ExportSettings Export,DateTime sDate,string folderPath) {
            ExportEvent exp = new ExportEvent();
            exp.ExportDate = sDate;
            exp.ClientID = this.ClientID;
            exp.Settings = Export;
            exp.FolderPath = folderPath;
            exp.Save();
            return exp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ImportEvent> GetPendingImportList() {
            Data.ImportExport ie = new Data.ImportExport();
            DataTable dt = ie.GetAllEvents(this.ClientID);
            var a = dt.toList<ImportEvent>(new DataFieldMappings().Add("ClientID", "ClientID")
                .Add("EventDateTime", "ExportDate")
                .Add("ImportExportID", "ExportImportID", true)
                 .Add("FilePath", "FolderPath")
                 .Add("Errors", "Errors")
                 .Add("Status", "Status")
                  .Add("IgnoreSQLErrors", "IgnoreSQLErrors"), null, (x, y) => dynamic(x, y));
            a =a.Where(x => x.Status == ImportExportStatus.pending).ToList();
            return a;
        }     
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ExportEvent> GetPendingExportList() {
           Data.ImportExport ie = new Data.ImportExport();
           DataTable dt= ie.GetAllEvents(this.ClientID);
            var a =dt.toList<ExportEvent>(new DataFieldMappings().Add("ClientID", "ClientID")
                .Add("EventDateTime", "ExportDate")
                .Add("ImportExportID", "ExportImportID", true)
                 .Add("FilePath", "FolderPath")              
                 .Add("Errors", "Errors")
                 .Add("Status", "Status")
                 .Add("Settings", "ExportSetting"), null, (x, y) => dynamic(x, y));
            a = a.Where(x => x.Status == ImportExportStatus.pending).ToList();
            return a;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private dynamic dynamic(string x, string y)
        {
            int k = 0;
            Int32.TryParse(y, out k);
            if (x == "Status")
            {
                return (ImportExportStatus)k;
            } if (x == "ExportSetting" || x== "Settings") {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<ExportSettings>(y);
            }
            else
            {
                return y;
            }
        }
    }

    public class ExportEvent {
        public string ExportImportID { get; set; }
        public string ClientID { get; set; }
        public ImportExportStatus Status { get; set;}
        public DateTime ExportDate { get; set; }
        public string FolderPath { get; set; }
        public ExportSettings Settings { get; set; }
        public string Errors { get; set; }
        public bool Save() {
            Data.ImportExport im = new Data.ImportExport();
           this.ExportImportID= im.SaveSchedule(this.ClientID,
               Newtonsoft.Json.JsonConvert.SerializeObject(Settings),0, ExportDate, FolderPath, false, 2);
            return true;
        }
        public bool ExecuteNow() {
            ClientServer sc = new ClientServer(this.ClientID);
            DataExport de = new DataExport(sc.GetServer().Connection());
            de.Merge<ExportSettings>(Settings);
            Data.ImportExport im = new Data.ImportExport();
            try
            {
                Status = ImportExportStatus.progress;
                im.UpdateScheduleStatus(this.ClientID, ExportImportID, (int)Status, "");
                var path = FolderPath + "/sqldump-" + Guid.NewGuid().ToString() + ".sql";
                de.ExportTo(path);
                Status = ImportExportStatus.completed;
                im.UpdateScheduleStatus(this.ClientID, ExportImportID, (int)Status, "", path);
            }
            catch (Exception ex)
            {
                Status = ImportExportStatus.error;
                im.UpdateScheduleStatus(this.ClientID, ExportImportID, (int)Status, "");
                throw ex;
            }
            finally {
              
            }         
            return true;
        }
    }

    public class ImportEvent {
        public string ExportImportID { get; set; }
        public string ClientID { get; set; }
        public ImportExportStatus Status { get; set; }
        public DateTime ImportDate { get; set; }
        public string ImportFilePath { get; set; }
        public bool IgnoreSQLErrors { get; set; }
        public bool Save()
        {
            //ClientServer sc = new ClientServer(this.ClientID);
            Data.ImportExport im = new Data.ImportExport();
        this.ExportImportID=    im.SaveSchedule(this.ClientID,
                "", 0, DateTime.Now, ImportFilePath, IgnoreSQLErrors, 1);
            return true;
        }
        public bool ImportNow() {
            ClientServer sc = new ClientServer(this.ClientID);
            DataImport di = new DataImport(sc.GetServer().Connection());
            Data.ImportExport im = new Data.ImportExport();

            try
            {
                Status = ImportExportStatus.progress;
                im.UpdateScheduleStatus(this.ClientID, ExportImportID, (int)Status, "");
              string rst = di.ImportFrom(ImportFilePath, IgnoreSQLErrors);
                if (rst == "done")
                {
                    Status = ImportExportStatus.completed;
                    im.UpdateScheduleStatus(this.ClientID, ExportImportID, (int)Status, "");
                    return true;
                }
                else
                {
                    Status = ImportExportStatus.error;
                    im.UpdateScheduleStatus(this.ClientID, ExportImportID, (int)Status, rst);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Status = ImportExportStatus.error;
                im.UpdateScheduleStatus(this.ClientID, ExportImportID, (int)Status, ex.Message);
                throw ex;
            }
            finally
            {
               
            }                               
        }
    }
}
