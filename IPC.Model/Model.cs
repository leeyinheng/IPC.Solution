using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPC.Models
{
    public class IPCRequest
    {
        public string ID { get; set; }

        public string UserName { get; set;}

        public string Password { get; set; }

    }

    public class IPCLog
    {
        public string IPCName { get; set; }

        public string Action { get; set;}

        public string Time { get; set;}

        public string Error { get; set;}
    }


    
}
