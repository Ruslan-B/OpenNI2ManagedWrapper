using System;

namespace OpenNI2
{
    internal unsafe static class OniCAPIExtensions
    {
        internal static void ThrowExectionIfStatusIsNotOk(this OniStatus status)
        {
            switch (status)
            {
                case OniStatus.ONI_STATUS_OK: 
                    return;
                case OniStatus.ONI_STATUS_NOT_IMPLEMENTED:
                    throw new NotImplementedException();
                case OniStatus.ONI_STATUS_NOT_SUPPORTED:
                    throw new NotSupportedException();
                case OniStatus.ONI_STATUS_ERROR: 
                case OniStatus.ONI_STATUS_BAD_PARAMETER:
                case OniStatus.ONI_STATUS_OUT_OF_FLOW:
                case OniStatus.ONI_STATUS_NO_DEVICE:
                    string message = OpenNI.GetExtendedError();
                    throw new OpenNIException(message);
                case OniStatus.ONI_STATUS_TIME_OUT:
                    throw new TimeoutException();
                default:
                    throw new ArgumentOutOfRangeException("status", string.Format("Unkown status :{0}.", status));
            }
        }

        internal static DeviceInfo ToManaged(this OniDeviceInfo oniDeviceInfo)
        {
            return new DeviceInfo
            {
                Uri = new String((sbyte*)oniDeviceInfo.uri),
                Vendor = new String((sbyte*)oniDeviceInfo.vendor),
                Name = new String((sbyte*)oniDeviceInfo.name),
                UsbVendorId = oniDeviceInfo.usbVendorId,
                UsbProductId = oniDeviceInfo.usbProductId
            };
        }

        internal static SensorInfo ToManaged(this OniSensorInfo oniSensorInfo)
        {
            VideoMode[] videoModes = new VideoMode[oniSensorInfo.numSupportedVideoModes];
            for (int i = 0; i < oniSensorInfo.numSupportedVideoModes; i++)
            {
                videoModes[i] = oniSensorInfo.pSupportedVideoModes[i].ToManaged();
            }

            return new SensorInfo
            {
                SensorType = oniSensorInfo.sensorType.ToManaged(),
                IsSupported = true,
                VideoModes = videoModes
            };
        }


    }
}

