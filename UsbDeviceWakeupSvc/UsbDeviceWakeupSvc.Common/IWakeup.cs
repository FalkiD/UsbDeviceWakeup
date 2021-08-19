using System.ServiceModel;

namespace UsbDeviceWakeupSvc.Common
{
    [ServiceContract]
    public interface IWakeup
    {
        [OperationContract]
        void Wakeup(string message);
    }

}
