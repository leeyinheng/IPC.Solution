using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
//using System.Threading.Tasks;

namespace IPC.SyncEngine
{
    public class SyncEngine : IDisposable
    {
        //private static readonly ManualResetEventSlim CancelEvent = new ManualResetEventSlim();

        private readonly SyncFilesManager _syncmanager;

        private readonly int frequencyByMins;

        private bool runservice; 

        public SyncEngine()
        {
            frequencyByMins = Convert.ToInt16(ConfigurationManager.AppSettings["ScheduledFrequencyByMins"]);

            string username = ConfigurationManager.AppSettings["username"];
            
            string password = ConfigurationManager.AppSettings["password"];

            string ftpsite = ConfigurationManager.AppSettings["ftpsite"];

            string localfolder = ConfigurationManager.AppSettings["localfolder"];

            _syncmanager = new SyncFilesManager(username , password , ftpsite, localfolder);

            runservice = true;  
        }
                

        public void Execute()
        {
            ThreadPool.QueueUserWorkItem(o => ScheduleSyncTask());
            
        }

        private void ScheduleSyncTask()
        {

           try
            {
                
                do
                {
                    // do work 
                    
                    _syncmanager.Execute();

                    Thread.Sleep((int)TimeSpan.FromMinutes(frequencyByMins).TotalMilliseconds); 
                                      

                   // SpinWait.SpinUntil((() => CancelEvent.IsSet), TimeSpan.FromMinutes(frequencyByMins));

                } while(runservice == true) ; //(!CancelEvent.IsSet)
            }
            catch (Exception ex)
            {
                // send log to web service 

                Console.WriteLine(ex.Message); 
            }
        }


        public void Dispose()
        {
            // send close notification 
            throw new NotImplementedException();
        }
    }
}
