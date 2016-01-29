using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
//using System.Threading.Tasks;

namespace IPC.SyncEngine
{
    public class SyncFilesManager : IDisposable
    {
        private readonly string username;
        private readonly string password;
        private readonly string ftpsite;
        private readonly string localfolder;
        private readonly string remotefolder;
        private readonly string[] syncfolders; 
        private IPC.Solution.FTPClient ftpClient;
        private bool mediaFilechange = false; 
        
        public SyncFilesManager(string _username, string _password, string _ftpsite, string _localfolder, string _remotefolder, string _syncfolders)
        {
             username = _username;

             password = _password;

             ftpsite = _ftpsite;

             localfolder = _localfolder;

             remotefolder = _remotefolder;

             syncfolders = _syncfolders.Split(';'); 

             ftpClient = new IPC.Solution.FTPClient(ftpsite, username, password); 
        }

        public void Execute()
        {
            //First kill BusSystem.exe to avoid file lock issue 
            System.Diagnostics.Process.Start("taskkill.exe", "/F /IM BusSystem.exe");

            foreach (var syncfolder in syncfolders)
            {
                var list = ftpClient.directoryListSimple(remotefolder + "//" + syncfolder);

                DownloadFiles(list, remotefolder + "//" + syncfolder, localfolder  + syncfolder);
            }

            //finally update Announcemant.csv 

            ftpClient.download("_Content//Announcemant.csv", localfolder + "Announcemant.csv");

            // reboot machine at the end 
               try
                {
                    //System.Diagnostics.Process.Start(@"C:\Program Files\BusSystem\BusSystem.exe");

                    System.Diagnostics.Process.Start("shutdown.exe", "-r -t 0");
                }
                catch (Exception ex)
                {
                     Console.WriteLine(ex.Message); 
                }
        }

     

        private void DownloadFiles(string[] list , string downloadpath, string localpath)
        {
            // download files from FTP 

            foreach (var file in list)
            {
                if (file != string.Empty)
                {
                    Console.WriteLine(file);

                    //mediaFilechange = true;

                    if (!System.IO.File.Exists(localpath + "//" + file))
                    {
                        ftpClient.download(downloadpath + "//" + file, localpath + "//" + file);

                    }

                }

            }

            // Remove files from local if not existis in FTP 

            var localfiles = Directory.GetFiles(localpath); 

            foreach (var localfile in localfiles)
            {
                var lfile = Path.GetFileName(localfile);

                if (!list.Contains(lfile))
                {
                    File.Delete(localpath + "//" + lfile); 
                }
            }
        }

        public void Dispose()
        {
            ftpClient = null; 
        }
    }
}
