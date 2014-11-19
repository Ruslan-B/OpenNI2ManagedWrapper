namespace OpenNI2
{
    public unsafe class VideoStream : DisposableBase
    {
        private readonly _OniStream* _pStream;

        internal VideoStream(_OniStream* pStream)
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

        public VideoFrame ReadFrame()
        {
            OniFrame* pFrame = null;
            OniCAPI.oniStreamReadFrame(_pStream, &pFrame).ThrowExectionIfStatusIsNotOk();
            return new VideoFrame(pFrame);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing == false)
                return;

            OniCAPI.oniStreamDestroy(_pStream);
        }
    }
}