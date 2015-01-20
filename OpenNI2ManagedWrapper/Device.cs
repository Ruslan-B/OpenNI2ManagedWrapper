﻿using System.Text;

namespace OpenNI2
{
    public unsafe class Device : DisposableBase
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
            _OniDevice* pDevice = null;
            byte[] bUri = Encoding.ASCII.GetBytes(uri);
            fixed (byte* pUri = bUri)
            {
                OniCAPI.oniDeviceOpen(pUri, &pDevice).ThrowExectionIfStatusIsNotOk();
            }
            return new Device(pDevice);
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

        protected override void Dispose(bool disposing)
        {
            if (disposing == false)
                return;
            OniCAPI.oniDeviceClose(_pDevice).ThrowExectionIfStatusIsNotOk();
        }
    }
}