using System;
using System.Management;
using NLog;
using UsbDeviceWakeupSvc.Common;

namespace UsbDeviceWakeupSvc.Service
{
    internal class UsbDeviceWakeup : IWakeup
    {
        static readonly Logger Nlog = LogManager.GetLogger("usb_wakeup");

        public void Wakeup(string device)
        {
            Console.WriteLine($"Wakeup:'{device}'.");
            Nlog.Info($"Wakeup:'{device}'.");
            string error = "";
            try
            {
                ManagementObjectSearcher mgmtObjSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption LIKE 'USB Input Device'");
                ManagementObjectCollection hidCol = mgmtObjSearcher.Get();
                if (hidCol == null)
                    error = "* Error: No USB Input devices found in Device Manager system *";
                else
                {
                    foreach (ManagementObject hidInstance in hidCol)
                    {
                        string deviceID = (string)hidInstance.GetPropertyValue("DeviceID");
                        if (deviceID.ToUpper().Contains("VID_0FFF"))
                        {
                            bool rebootNeeded = true;
                            object[] args = new object[] { rebootNeeded };
                            Console.WriteLine("Disable/enable USB device will take a few seconds...");
                            Nlog.Info("Disable/enable USB device will take a few seconds...");
                            System.Threading.Thread.Sleep(250);
                            var abc = hidInstance.InvokeMethod("Disable", args);
                            System.Threading.Thread.Sleep(2000);
                            abc = hidInstance.InvokeMethod("Enable", args);
                            Console.WriteLine("USB device has been re-enabled");
                            Nlog.Info("USB device has been re-enabled");
                            return;
                        }
                    }
                    error = "* Error: USB Input device VID_0FFF was not found in local Device Manager system *";
                }
            }
            catch (Exception ex)
            {
                error = string.Format("* Exception finding/disabling/enabling USB device:{0} *", ex.Message);
                if (ex.Message.Contains("Generic"))
                    error += string.Format("{0}(This application MUST be run as Administrator for disable/enable device to work)",
                                                Environment.NewLine);
            }
            Console.WriteLine(error);
            Nlog.Error(error);
        }
    }
}