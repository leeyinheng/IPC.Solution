using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace IPC.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var syncEngine = new IPC.SyncEngine.SyncEngine();

                syncEngine.Execute();

                Console.WriteLine("Press enter key to stop program");

                Console.ReadLine(); 
            }
            catch (Exception ex)
            {

                Console.Write(ex.Message); 
            }

          
        }
    }
}
