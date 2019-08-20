using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
 
using System.Threading.Tasks;
using System.Timers;

namespace Tz.DataService
{
    public partial class BackupService : ServiceBase
    {
        private int eventId = 1;

        public BackupService()
        {
            InitializeComponent();
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("tzDataBackup"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "tzDataBackup", "tzDataLog");
            }
            eventLog1.Source = "tzDataBackup";
            eventLog1.Log = "tzDataLog";
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("Tz DataBackup service in OnStart.");

            Timer timer = new Timer();
            timer.Interval = 60000; // 60 seconds
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();            
        }
        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        }


        protected override void OnStop()
        {

        }
    }
}
