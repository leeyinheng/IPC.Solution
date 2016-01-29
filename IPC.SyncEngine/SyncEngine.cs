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

        private readonly CheckVersionManager _versionmanger; 

        private readonly int frequencyByMins;

        private bool runservice; 

        public SyncEngine()
        {
            frequencyByMins = Convert.ToInt16(ConfigurationManager.AppSettings["ScheduledFrequencyByMins"]);

            string username = ConfigurationManager.AppSettings["username"];
            
            string password = ConfigurationManager.AppSettings["password"];

            string ftpsite = ConfigurationManager.AppSettings["ftpsite"];

            string localfolder = ConfigurationManager.AppSettings["contentfolder"];

            string remoteversionfolder = ConfigurationManager.AppSettings["remoteversionfolder"];

            string localversionfolder = ConfigurationManager.AppSettings["localversionfolder"];

            string remotecontentfolder = ConfigurationManager.AppSettings["remotecontentfolder"];

            string syncfolders = ConfigurationManager.AppSettings["syncfolders"];

            _syncmanager = new SyncFilesManager(username , password , ftpsite, localfolder, remotecontentfolder, syncfolders);

            _versionmanger = new CheckVersionManager(username, password, ftpsite, remoteversionfolder, localversionfolder); 

            runservice = true;  
        }

        public void Execute()
        {
            ThreadPool.QueueUserWorkItem(o => ScheduleSyncTask());
        }

        private void ScheduleSyncTask()
        {
                        
                do
                {
                    // do work 

                    try
                    {
                        Console.WriteLine(System.Environment.MachineName + " - Sync Operation Starts at " + DateTime.Now.ToShortTimeString());

                        if (_versionmanger.IsVersionDifferent() == true)
                        {
                            _syncmanager.Execute();

                            //send log 
                        }
                        else
                        {

                            //send log 
                        }
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Error-" + ex.Message);
                    }
                      
                    Thread.Sleep((int)TimeSpan.FromMinutes(frequencyByMins).TotalMilliseconds); 
                                    
                   // SpinWait.SpinUntil((() => CancelEvent.IsSet), TimeSpan.FromMinutes(frequencyByMins));

                } while(runservice == true) ; //(!CancelEvent.IsSet)
            }
         
        public void Dispose()
        {
            // send close notification 
            //throw new NotImplementedException();
            runservice = false;
  
        }
    }
}
