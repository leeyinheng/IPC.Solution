using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace IPC.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ServiceController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/Service/5
        [HttpGet("{id}")]
        public string Get(string id , string username , string password)
        {
            var x = id; 

            return "value";
        }

        // POST api/service
        [HttpPost]
        public void Post([FromBody]string value)
        {
            var x = value; 
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private bool CheckSchedule(string id)
        {
            var x = new Models.ScheduleModel(); 

            return true; 

        }
    }
}
