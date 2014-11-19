using System;
using System.IO;
using System.Runtime.InteropServices;

namespace OpenNI2
{
    public unsafe class VideoFrame : DisposableBase
    {
        private readonly OniFrame* _pFrame;
        private readonly FrameData _data;

        internal VideoFrame(OniFrame* pFrame)
        {
            _pFrame = pFrame;
            _data = new FrameData(_pFrame->data, _pFrame->dataSize);
        }

        public FrameData Data
        {
            get { return _data; }
        }

        public SensorType SensorType
        {
            get { return _pFrame->sensorType.ToManaged(); }
        }

        public ulong Timestamp
        {
            get { return _pFrame->timestamp; }
        }

        public int FrameIndex
        {
            get { return _pFrame->frameIndex; }
        }

        public int Width
        {
            get { return _pFrame->width; }
        }

        public int Height
        {
            get { return _pFrame->height; }
        }

        public VideoMode VideoMode
        {
            get { return _pFrame->videoMode.ToManaged(); }
        }

        public bool CroppingEnabled
        {
            get { return _pFrame->croppingEnabled > 0; }
        }

        public int CropOriginX
        {
            get { return _pFrame->cropOriginX; }
        }

        public int CropOriginY
        {
            get { return _pFrame->cropOriginY; }
        }

        public int Stride
        {
            get { return _pFrame->stride; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing == false)
                return;
            OniCAPI.oniFrameRelease(_pFrame);
        }

        public class FrameData
        {
            private readonly byte* _pData;
            private readonly int _size;

            internal FrameData(byte* pData, int size)
            {
                _pData = pData;
                _size = size;
            }

            public IntPtr Handler
            {
                get { return (IntPtr) _pData; }
            }

            public int Size
            {
                get { return _size; }
            }

            public Stream CreateStream()
            {
                return new UnmanagedMemoryStream(_pData, _size);
            }

            public void CopyTo(byte[] buffer)
            {
                Marshal.Copy(Handler, buffer, 0, buffer.Length);
            }
        }
    }
}