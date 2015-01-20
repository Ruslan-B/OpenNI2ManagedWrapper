using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace OpenNI2
{

    public unsafe partial class Device : DisposableBase
    {
        private readonly _OniDevice* _pDevice;

        internal Device(_OniDevice* pDevice)
        {
            _pDevice = pDevice;
        }

        public static Device Open()
        {
            _OniDevice* pDevice = null;
            OniCAPI.oniDeviceOpen(null, &pDevice).ThrowExectionIfStatusIsNotOk();
            return new Device(pDevice);
        }

        public static Device Open(string uri)
        {
            IntPtr pUri = Marshal.StringToHGlobalAnsi(uri);
            try
            {
                _OniDevice* pDevice = null;
                OniCAPI.oniDeviceOpen((byte*)pUri, &pDevice).ThrowExectionIfStatusIsNotOk();
                return new Device(pDevice);
            }
            finally
            {
                Marshal.FreeHGlobal(pUri);
            }
        }

        internal static Device Open(string uri, string mode)
        {
            IntPtr pUri = Marshal.StringToHGlobalAnsi(uri);
            IntPtr pMode = Marshal.StringToHGlobalAnsi(mode);
            try
            {
                _OniDevice* pDevice = null;
                OniCAPI.oniDeviceOpenEx((byte*)pUri, (byte*)pMode, &pDevice).ThrowExectionIfStatusIsNotOk();
                return new Device(pDevice);
            }
            finally
            {
                Marshal.FreeHGlobal(pUri);
                Marshal.FreeHGlobal(pMode);
            }
        }

        public DeviceInfo GetDeviceInfo()
        {
            var oniDeviceInfo = new OniDeviceInfo();
            OniCAPI.oniDeviceGetInfo(_pDevice, &oniDeviceInfo).ThrowExectionIfStatusIsNotOk();
            DeviceInfo deviceInfo = oniDeviceInfo.ToManaged();
            return deviceInfo;
        }

        public SensorInfo GetSensorInfo(SensorType sensorType)
        {
            OniSensorInfo* pOniSensorInfo = OniCAPI.oniDeviceGetSensorInfo(_pDevice, sensorType.ToNative());
            if (pOniSensorInfo == null)
                return new SensorInfo {SensorType = sensorType};

            OniSensorInfo oniSensorInfo = *pOniSensorInfo;
            SensorInfo sensorInfo = oniSensorInfo.ToManaged();
            return sensorInfo;
        }

        public bool HasSensor(SensorType sensorType)
        {
            return OniCAPI.oniDeviceGetSensorInfo(_pDevice, sensorType.ToNative()) != null;
        }

        public SensorStream CreateStream(SensorType sensorType)
        {
            _OniStream* pStream = null;
            OniCAPI.oniDeviceCreateStream(_pDevice, sensorType.ToNative(), &pStream).ThrowExectionIfStatusIsNotOk();

            return new SensorStream(pStream);
        }
        public bool IsPropertySupported(int propertyId)
        {
            return OniCAPI.oniDeviceIsPropertySupported(_pDevice, propertyId) == 1;
        }

        public T GetProperty<T>(int propertyId) where T : struct
        {
            int dataSize = Marshal.SizeOf(typeof(T));

            IntPtr pData = Marshal.AllocHGlobal(dataSize);
            try
            {
                OniCAPI.oniDeviceGetProperty(_pDevice, propertyId, (void*)pData, &dataSize).ThrowExectionIfStatusIsNotOk();

                Debug.Assert(dataSize == Marshal.SizeOf(typeof(T)), "dataSize == Marshal.SizeOf(typeof(T))");

                var value = (T)Marshal.PtrToStructure(pData, typeof(T));
                return value;
            }
            finally
            {
                Marshal.FreeHGlobal(pData);
            }
        }

        public void SetProperty<T>(int propertyId, T value) where T : struct
        {
            int dataSize = Marshal.SizeOf(typeof(T));

            IntPtr pData = Marshal.AllocHGlobal(Marshal.SizeOf(dataSize));
            try
            {
                Marshal.StructureToPtr(value, pData, false);
                OniCAPI.oniDeviceSetProperty(_pDevice, propertyId, (void*)pData, dataSize).ThrowExectionIfStatusIsNotOk();
            }
            finally
            {
                Marshal.FreeHGlobal(pData);
            }
        }

        public bool IsCommandSupported(int commandId)
        {
            return OniCAPI.oniDeviceIsCommandSupported(_pDevice, commandId) == 1;
        }

        public void Invoke<T>(int commandId, T command) where T : struct
        {
            int dataSize = Marshal.SizeOf(typeof(T));

            IntPtr pData = Marshal.AllocHGlobal(Marshal.SizeOf(dataSize));
            try
            {
                Marshal.StructureToPtr(command, pData, false);
                OniCAPI.oniDeviceInvoke(_pDevice, commandId, (void*)pData, dataSize).ThrowExectionIfStatusIsNotOk();
            }
            finally
            {
                Marshal.FreeHGlobal(pData);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing == false)
                return;
            OniCAPI.oniDeviceClose(_pDevice).ThrowExectionIfStatusIsNotOk();
        }
    }
}