using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

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

            string site = @"http://localhost:63408/api";

            string servicesite = string.Format("{0}/{1}/", site, "service");

            string URI = servicesite;
            string value = "test";

            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string HtmlResult = wc.UploadString(URI, value);
            }

        }
    }
}
