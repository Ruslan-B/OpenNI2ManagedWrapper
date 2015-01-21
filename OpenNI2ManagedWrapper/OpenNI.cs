using System;
using System.Runtime.InteropServices;

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

        public static OpenNIVersion GetVersion()
        {
            return OniCAPI.oniGetVersion().ToManaged();
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

        public static void SetLogMinSeverity(int minSeverity) 
        {
            OniCAPI.oniSetLogMinSeverity(minSeverity).ThrowExectionIfStatusIsNotOk();
        }

        public static void SetLogConsoleOutput(bool consoleOutput) 
        {
            OniCAPI.oniSetLogConsoleOutput(consoleOutput ? 1 : 0).ThrowExectionIfStatusIsNotOk();
        }

        public static void SetLogFileOutput(bool fileOutput) 
        {
            OniCAPI.oniSetLogFileOutput(fileOutput ? 1 : 0).ThrowExectionIfStatusIsNotOk();
        }

        public static unsafe void SetLogOutputFolder(string outputFolder)
        {
            IntPtr pOutputFolder = Marshal.StringToHGlobalAnsi(outputFolder);
            try
            {
                OniCAPI.oniSetLogOutputFolder((byte*)pOutputFolder).ThrowExectionIfStatusIsNotOk();
            }
            finally
            {
                Marshal.FreeHGlobal(pOutputFolder);
            }
        }

        public static unsafe string GetLogFileName() 
        {
            const int MAX_PATH = 260;
            IntPtr pFileName = Marshal.AllocHGlobal(MAX_PATH);
            try
            {
                OniCAPI.oniGetLogFileName((byte*)pFileName, MAX_PATH).ThrowExectionIfStatusIsNotOk();
                string fileName = Marshal.PtrToStringAnsi(pFileName);
                return fileName;
            }
            finally
            {
                Marshal.FreeHGlobal(pFileName);
            }
        }
    }
}