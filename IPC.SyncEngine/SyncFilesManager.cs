using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace IPC.SyncEngine
{
    public class SyncFilesManager
    {
        private readonly string username;
        private readonly string password;
        private readonly string ftpsite;
        private readonly string localfolder; 

        public SyncFilesManager(string _username, string _password, string _ftpsite, string _localfolder)
        {
             username = _username;

             password = _password;

             ftpsite = _ftpsite;

             localfolder = _localfolder; 
        }

        public void Execute()
        {
            var ftpClient = new IPC.Solution.FTPClient(ftpsite, username, password);
           
            var list = ftpClient.directoryListSimple("\\");
            
            foreach (var file in list)
            {
                if (file != string.Empty)
                {
                    Console.WriteLine(file);

                    ftpClient.download(file, localfolder + file);
                }

                //var filesize = ftpClient.getFileCreatedDateTime(file);

                //Console.WriteLine(filesize); 
            }  
        }

        private string GetVersionNumber()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            return assembly.GetName().Version.ToString();
        }
    }
}
