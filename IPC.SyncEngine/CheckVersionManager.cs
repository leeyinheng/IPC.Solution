using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IPC.SyncEngine
{
    public class CheckVersionManager
    {
        private readonly string username;
        private readonly string password;
        private readonly string ftpsite;
        private readonly string remoteversionfolder;
        private readonly string localversionfolder; 

        public CheckVersionManager(string _username, string _password, string _ftpsite, string _remoteversionfolder, string _localversionfolder)
        {
            username = _username;

            password = _password;

            ftpsite = _ftpsite;

            remoteversionfolder = _remoteversionfolder;

            localversionfolder = _localversionfolder; 
        }


        public bool IsVersionDifferent()
        {
            DirectoryInfo di = new DirectoryInfo(localversionfolder);

            if (di.Exists == false)
            {
                Directory.CreateDirectory(localversionfolder);
            }

            var localversion =  Path.GetFileName(System.IO.Directory.GetFiles(localversionfolder).FirstOrDefault());

            var ftpClient = new IPC.Solution.FTPClient(ftpsite, username, password);

            var remoteversion = ftpClient.directoryListSimple(remoteversionfolder).FirstOrDefault(); 
            
            if (localversion == null)
            {
                // copy version to local 
       
                System.IO.File.Create(localversionfolder + remoteversion);

                return true; 
            }
            else
            {
                if (localversion == remoteversion)
                {
                    return false; // do not Sync 
                }
                else
                {
                    // remove old version file 
                    System.IO.File.Delete(localversionfolder + localversion);

                    // copy new version file to local folder 
                    System.IO.File.Create(localversionfolder + remoteversion);
                    return true;
                }
            }


        } 
    }
}
