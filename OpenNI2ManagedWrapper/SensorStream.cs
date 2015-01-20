namespace OpenNI2
{
    public unsafe class SensorStream : DisposableBase
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
       
        protected override void Dispose(bool disposing)
        {
            if (disposing == false)
                return;

            OniCAPI.oniStreamDestroy(_pStream);
        }
    }
}