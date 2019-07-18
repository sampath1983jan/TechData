using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.Net.DataSchema
{
    public class DataImport
    {
        private string Conn;
        public DataImport(string server)
        {
            if (server.EndsWith(";") == false)
            {
                server = server + ";";
            }
            server = server + "charset=utf8;convertzerodatetime=true;";
            Conn = server;
        }
        public string ImportFrom(string filePath, bool ignoreError)
        {
            try
            {
                Data.ImportExport im = new Data.ImportExport();
                return im.Import(Conn, ignoreError, filePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
