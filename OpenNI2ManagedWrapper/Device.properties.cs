using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace OpenNI2
{
    partial class Device 
    {
        public bool IsDriverVersionPropertySupported
        {
            get { return IsPropertySupported(OniCAPI.ONI_DEVICE_PROPERTY_DRIVER_VERSION); }
        }

        public OpenNIVersion DriverVersion 
        {
            get { return GetProperty<OniVersion>(OniCAPI.ONI_DEVICE_PROPERTY_DRIVER_VERSION).ToManaged(); }
        }

        public bool IsHardwareVersionPropertySupported
        {
            get { return IsPropertySupported(OniCAPI.ONI_DEVICE_PROPERTY_DRIVER_VERSION); }
        }

        public int HardwareVersion 
        {
            get { return GetProperty<int>(OniCAPI.ONI_DEVICE_PROPERTY_HARDWARE_VERSION); }
        }

        public bool IsSerialNumberPropertySupported
        {
            get { return IsPropertySupported(OniCAPI.ONI_DEVICE_PROPERTY_SERIAL_NUMBER); }
        }

        public unsafe string SerialNumber 
        {
            get 
            {
                const int MAX_STRING_LENGTH = 200;
                int dataSize = MAX_STRING_LENGTH;
                IntPtr pData = Marshal.AllocHGlobal(dataSize);
                try
                {
                    OniCAPI.oniDeviceGetProperty(_pDevice, OniCAPI.ONI_DEVICE_PROPERTY_SERIAL_NUMBER, (void*)pData, &dataSize).ThrowExectionIfStatusIsNotOk();
                    string value = Marshal.PtrToStringAnsi(pData);
                    return value;
                }
                finally
                {
                    Marshal.FreeHGlobal(pData);
                }
            }
        }

        public bool IsImageRegistrationPropertySupported
        {
            get { return IsPropertySupported(OniCAPI.ONI_DEVICE_PROPERTY_SERIAL_NUMBER); }
        }

        // todo ONI_DEVICE_PROPERTY_IMAGE_REGISTRATION (OniImageRegistrationMode)

        public unsafe bool DepthColorSync
        {
            get { return OniCAPI.oniDeviceGetDepthColorSyncEnabled(_pDevice) == 1; }
            set {
                if (value)
                {
                    OniCAPI.oniDeviceEnableDepthColorSync(_pDevice).ThrowExectionIfStatusIsNotOk();
                } else {
                    OniCAPI.oniDeviceDisableDepthColorSync(_pDevice);
                }
            }
        }
    }
}