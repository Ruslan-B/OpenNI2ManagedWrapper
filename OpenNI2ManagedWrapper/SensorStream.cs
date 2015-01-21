using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace OpenNI2
{
    public unsafe partial class SensorStream : DisposableBase
    {
        internal readonly _OniStream* _pStream;

        internal SensorStream(_OniStream* pStream)
        {
            _pStream = pStream;
        }

        public SensorInfo GetSensorInfo()
        {
            OniSensorInfo* pOniSensorInfo = OniCAPI.oniStreamGetSensorInfo(_pStream);
            OniSensorInfo oniSensorInfo = *pOniSensorInfo;
            SensorInfo sensorInfo = oniSensorInfo.ToManaged();
            return sensorInfo;
        }

        public void Start()
        {
            OniCAPI.oniStreamStart(_pStream).ThrowExectionIfStatusIsNotOk();
        }

        public void Stop()
        {
            OniCAPI.oniStreamStop(_pStream);
        }

        public SensorFrame ReadFrame()
        {
            OniFrame* pFrame = null;
            OniCAPI.oniStreamReadFrame(_pStream, &pFrame).ThrowExectionIfStatusIsNotOk();
            return new SensorFrame(pFrame);
        }
       
        public bool IsPropertySupported(int propertyId)
        {
            return OniCAPI.oniStreamIsPropertySupported(_pStream, propertyId) == 1;
        }

        public T GetProperty<T>(int propertyId) where T : struct
        {
            int dataSize = Marshal.SizeOf(typeof(T));

            IntPtr pData = Marshal.AllocHGlobal(dataSize);
            try
            {
                OniCAPI.oniStreamGetProperty(_pStream, propertyId, (void*)pData, &dataSize).ThrowExectionIfStatusIsNotOk();

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
                OniCAPI.oniStreamSetProperty(_pStream, propertyId, (void*)pData, dataSize).ThrowExectionIfStatusIsNotOk();
            }
            finally
            {
                Marshal.FreeHGlobal(pData);
            }
        }

        public bool IsCommandSupported(int commandId)
        {
            return OniCAPI.oniStreamIsCommandSupported(_pStream, commandId) == 1;
        }

        public void Invoke<T>(int commandId, T command) where T : struct
        {
            int dataSize = Marshal.SizeOf(typeof(T));

            IntPtr pData = Marshal.AllocHGlobal(Marshal.SizeOf(dataSize));
            try
            {
                Marshal.StructureToPtr(command, pData, false);
                OniCAPI.oniStreamInvoke(_pStream, commandId, (void*)pData, dataSize).ThrowExectionIfStatusIsNotOk();
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

            OniCAPI.oniStreamDestroy(_pStream);
        }
    }
}