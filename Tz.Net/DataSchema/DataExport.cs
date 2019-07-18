 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.Net.DataSchema
{
    public enum ExportType {
        structureonly,
        structurewithdata
    }
    public enum BlobDataExportMode {
        HexString = 1,
        BinaryChar = 2
    }
    public class ExportSettings {
        public bool EnableDropDatabase { get; set; }
        public bool EnableCreateDatabase { get; set; }
        public bool EnableDropTable { get; set; }
        public ExportType Type { get; set; }
        public bool EnableExportFunction { get; set; }
        public bool EnableExportStoredProcedure { get; set; }
        public bool EnableExportTrigger { get; set; }
        public bool EnableExportEvent { get; set; }
        public bool EnableExportViews { get; set; }
        public BlobDataExportMode BlobExportMode { get; set; }
        public ExportSettings() {
            EnableCreateDatabase = true;
            EnableDropDatabase = true;
            EnableDropTable = true;
            EnableExportFunction = true;
            EnableExportStoredProcedure = true;
            EnableExportTrigger = true;
            EnableExportEvent = true;
            EnableExportViews = true;
            BlobExportMode = BlobDataExportMode.HexString;
            Type = ExportType.structurewithdata;
        }
    }
   public class DataExport: ExportSettings
    {       
        /// <summary>
        /// Set or Gets maximum length for combining multiple INSERTs into single sql
        /// </summary>
        public int MaxSQLLength { get; set; }
        private string Conn;
        
        public DataExport(string server) {         
            if (server.EndsWith(";") == false)
            {
                server = server + ";";
            }
            server = server + "charset=utf8;convertzerodatetime=true;";
            Conn = server;
        }
        public bool ExportTo( string folder) {
          
            Data.ImportExport im = new Data.ImportExport();
            bool exTable = false;
            bool exRow = false;
            if (Type == ExportType.structureonly) {
                exTable = true;
                exRow = false;
            } else {
                exTable = true;
                exRow = true;
            }
            try
            {
                return im.Export(Conn,
                folder,
                EnableDropDatabase,
                EnableCreateDatabase,
                EnableDropDatabase,
                exTable,
                exRow,
                EnableExportFunction,
                EnableExportStoredProcedure,
                EnableExportTrigger,
                EnableExportEvent,
                EnableExportViews,
                MaxSQLLength,
                (int)BlobExportMode);
            }
            catch (Exception ex) {
                throw ex;
            }         
        }

      
    }
}
