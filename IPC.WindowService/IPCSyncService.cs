using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
//using System.Threading.Tasks;

namespace IPC.WindowService
{
    public partial class IPCSyncService : ServiceBase
    {
        private static IPC.SyncEngine.SyncEngine syncEngine; 

        public IPCSyncService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            syncEngine = new SyncEngine.SyncEngine();

            syncEngine.Execute(); 
             
        }

        protected override void OnStop()
        {
            syncEngine.Dispose(); 
        }
    }
}
