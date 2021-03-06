﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IPC.Test
{
    [TestClass]
    public class UnitTestWebApi
    {
        [TestMethod]
        public void TestPost()
        {
            string username = "test";

            string password = "test";

            string site = @"http://localhost:53226/api";

            string servicesite = string.Format("{0}/{1}/", site, "service");

            string URI = servicesite;
            string value = "test";

            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string HtmlResult = wc.UploadString(URI, value);
            }

        }

        [TestMethod]
        public void TestIPCLogging()
        {
            RunAsync().Wait(); 
        }

        private async Task RunAsync()
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
                var response = await client.PostAsJsonAsync("api/products", newlog);
                if (response.IsSuccessStatusCode)
                {
                    
                }
            }
        }
    }
}
