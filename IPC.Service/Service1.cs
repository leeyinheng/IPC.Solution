using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace IPC.Service
{
    public partial class Service1 : ServiceBase
    {
        private static IPC.SyncEngine.SyncEngine syncEngine;

        public Service1()
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
