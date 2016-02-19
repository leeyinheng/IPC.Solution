using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IPC.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            var newlog = new IPC.Models.IPCLog();

            newlog.Action = "Check";
            newlog.Error = string.Empty;
            newlog.IPCName = "IPCTest";
            newlog.Time = $"{DateTime.Now.ToShortDateString()}-{DateTime.Now.ToShortTimeString()}";

            string site = @"http://localhost:53226/api";

            string servicesite = string.Format("{0}/{1}/", site, "logging");

            string URI = servicesite;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(servicesite);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                var response = await client.PostAsJsonAsync(servicesite, newlog);
                if (response.IsSuccessStatusCode)
                {

                }
            }
        }
    }
}
