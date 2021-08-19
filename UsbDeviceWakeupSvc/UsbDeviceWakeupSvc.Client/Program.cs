using System;
using System.ServiceModel;
using UsbDeviceWakeupSvc.Common;

namespace UsbDeviceWakeupSvc.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var channelFactory = new ChannelFactory<IWakeup>("NetTcpEndpoint"))
            {
                var proxy = channelFactory.CreateChannel();
                string msg = Guid.NewGuid().ToString();
                Console.WriteLine($"Calling '{nameof(IWakeup.Wakeup)}' over TCP with message '{msg}' via ChannelFactory.");
                proxy.Wakeup(msg);
            }

            using (var channelFactory = new ChannelFactory<IWakeup>("NamedPipeEndpoint"))
            {
                var proxy = channelFactory.CreateChannel();
                string msg = Guid.NewGuid().ToString();
                Console.WriteLine($"Calling '{nameof(IWakeup.Wakeup)}' over named pipe with message '{msg}' via ChannelFactory.");
                proxy.Wakeup(msg);
            }
        }
    }
}
