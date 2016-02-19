using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPC.WebApi4.Controllers
{
    public class LoggingController : ApiController
    {
        private Object thisLock = new Object();

        private readonly Common.Azure.CloudTableOperation<Common.Model.IPCLog> op =  new Common.Azure.CloudTableOperation<Common.Model.IPCLog>("IPCLogs");

       
        // POST: api/Logging
        public void Post([FromBody] IPC.Models.IPCLog log)
        {
           
            var newlog = new Common.Model.IPCLog(log.IPCName, log.Time);
            newlog.Action = log.Action;
            newlog.Error = log.Error;

            lock (thisLock)
            {
                op.AddorUpdateEntity(newlog);
            }

        }

      
    }
}
