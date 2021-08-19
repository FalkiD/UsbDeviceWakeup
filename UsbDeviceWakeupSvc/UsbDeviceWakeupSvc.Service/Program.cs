using System;
using System.ServiceModel;


namespace UsbDeviceWakeupSvc.Service
{
    class Program
    {
        private static void Main(string[] args)
        {
            var serviceHost = new ServiceHost(typeof(UsbDeviceWakeup));
            serviceHost.Open();

            Console.WriteLine("Service is running. Press any key to exit...");
            Console.ReadKey();
        }
    }
}
