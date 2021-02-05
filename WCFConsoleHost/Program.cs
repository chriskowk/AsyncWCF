using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WCFConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServiceHost host = new ServiceHost(typeof(WCFService.AddService));
                host.OpenTimeout = TimeSpan.FromMinutes(10);
                host.Open();
                DisplayHostInfo(host);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Press <Enter> key to exit ...");
                Console.ReadLine();
            }
        }

        private static void DisplayHostInfo(ServiceHost svcHost)
        {
            Console.WriteLine("Display HostInfo ......");
            Console.WriteLine("***********************************************************************");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Service Started {0}", svcHost.Description.ServiceType.FullName);
            Console.ResetColor();
            Console.WriteLine("***********************************************************************");
            Console.WriteLine(string.Empty);

            int count = 1;
            foreach (ServiceEndpoint endPnt in svcHost.Description.Endpoints)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine("--------------------------Endpoint {0}--------------------------------", count++);
                Console.WriteLine(string.Empty);
                Console.WriteLine("End Point Name          : {0}", endPnt.Name);
                Console.WriteLine("Binding                 : {0}", endPnt.Binding.Name);
                Console.WriteLine("End Point Address       : {0}", endPnt.Address);
                Console.WriteLine(string.Empty);
                Console.WriteLine("--------------------------------------------------------------------");
                Console.WriteLine(string.Empty);
            }
            Console.WriteLine("***********************************************************************");
            Console.WriteLine(string.Empty);
        }
    }
}
