using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using Tz.Net.DataSchema;

namespace Tz.BackApp.Models
{
    public class Worker
    {
        string filePath = @"d:/test.txt";

        public void StartProcessing(CancellationToken cancellationToken = default(CancellationToken),
            string clientid="",ExportSettings exportSettings=null)
        {
            Tz.Net.DataSchema.ImportExport iex = new Net.DataSchema.ImportExport(clientid);
            if (exportSettings == null) {
                exportSettings = new ExportSettings();
            }
            ExportEvent exportEvent = iex.ExportScheduleNow(exportSettings, @"D:/temp");
            exportEvent.ExecuteNow();
        }
        private void ProcessCancellation()
        {
            Thread.Sleep(10000);
            File.AppendAllText(filePath, "Process Cancelled");
        }

        public void StartImportProcess(CancellationToken cancellationToken = default(CancellationToken),
            string clientid="",bool ignoreError=false,string path="")
        {

            Tz.Net.DataSchema.ImportExport iex = new Net.DataSchema.ImportExport(clientid);
           ImportEvent impe= iex.ImportScheduleNow(ignoreError, path);
            impe.ImportNow();
        }
    }

}