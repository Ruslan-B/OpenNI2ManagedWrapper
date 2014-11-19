using System;

namespace OpenNI2
{
    public static class OpenNI
    {
        internal const int ONI_VERSION_MAJOR = 2;
        internal const int ONI_VERSION_MINOR = 2;
        internal const int ONI_API_VERSION = ((ONI_VERSION_MAJOR)*1000 + (ONI_VERSION_MINOR));

        public static unsafe string GetExtendedError()
        {
            byte* pMessage = OniCAPI.oniGetExtendedError();
            return new String((sbyte*) pMessage);
        }

        public static void Initialize()
        {
            OniCAPI.oniInitialize(ONI_API_VERSION).ThrowExectionIfStatusIsNotOk();
        }

        public static void Shutdown()
        {
            OniCAPI.oniShutdown();
        }

        public static unsafe DeviceInfo[] GetDevices()
        {
            OniDeviceInfo* pDevices = null;
            int count = 0;

            OniCAPI.oniGetDeviceList(&pDevices, &count).ThrowExectionIfStatusIsNotOk();

            var devices = new DeviceInfo[count];
            for (int i = 0; i < count; i++)
            {
                OniDeviceInfo oniDeviceInfo = pDevices[i];

                devices[i] = oniDeviceInfo.ToManaged();
            }

            OniCAPI.oniReleaseDeviceList(pDevices).ThrowExectionIfStatusIsNotOk();

            return devices;
        }
    }
}