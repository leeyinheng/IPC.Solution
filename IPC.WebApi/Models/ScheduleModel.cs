using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPC.WebApi.Models
{
    public class ScheduleModel
    {
       public string ID { get; set; }

       public string[] Schedule { get; set; }
    }
}
