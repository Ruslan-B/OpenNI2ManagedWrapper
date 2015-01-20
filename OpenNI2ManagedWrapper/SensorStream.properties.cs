using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace OpenNI2
{
    partial class SensorStream
    {
        public bool IsCroppingPropertySupported
        {
            get { return IsPropertySupported(OniCAPI.ONI_STREAM_PROPERTY_HORIZONTAL_FOV); }
        }

        //todo internal const int ONI_STREAM_PROPERTY_CROPPING (OniCropping*)

        public bool IsHorizontalFovPropertySupported
        {
            get { return IsPropertySupported(OniCAPI.ONI_STREAM_PROPERTY_HORIZONTAL_FOV); }
        }

        public float HorizontalFov
        {
            get { return GetProperty<float>(OniCAPI.ONI_STREAM_PROPERTY_HORIZONTAL_FOV); }
        }

        public bool IsVerticalFovPropertySupported
        {
            get { return IsPropertySupported(OniCAPI.ONI_STREAM_PROPERTY_VERTICAL_FOV); }
        }

        public float VerticalFov
        {
            get { return GetProperty<float>(OniCAPI.ONI_STREAM_PROPERTY_VERTICAL_FOV); }
        }

        public bool IsVideoModePropertySupported
        {
            get { return IsPropertySupported(OniCAPI.ONI_STREAM_PROPERTY_VIDEO_MODE); }
        }

        public VideoMode VideoMode 
        {
            get { return GetProperty<OniVideoMode>(OniCAPI.ONI_STREAM_PROPERTY_VIDEO_MODE).ToManaged(); }
            set { SetProperty<OniVideoMode>(OniCAPI.ONI_STREAM_PROPERTY_VIDEO_MODE, value.ToNative()); }
        }

        public bool IsMaxValuePropertySupported
        {
            get { return IsPropertySupported(OniCAPI.ONI_STREAM_PROPERTY_MAX_VALUE); }
        }

        public int MaxValue
        {
            get { return GetProperty<int>(OniCAPI.ONI_STREAM_PROPERTY_MAX_VALUE); }
        }

        public bool IsMinValuePropertySupported
        {
            get { return IsPropertySupported(OniCAPI.ONI_STREAM_PROPERTY_MIN_VALUE); }
        }

        public int MinValue
        {
            get { return GetProperty<int>(OniCAPI.ONI_STREAM_PROPERTY_MIN_VALUE); }
        }

        public bool IsStridePropertySupported
        {
            get { return IsPropertySupported(OniCAPI.ONI_STREAM_PROPERTY_STRIDE); }
        }

        public int Stride
        {
            get { return GetProperty<int>(OniCAPI.ONI_STREAM_PROPERTY_STRIDE); }
        }

        public bool IsMirroringPropertySupported
        {
            get { return IsPropertySupported(OniCAPI.ONI_STREAM_PROPERTY_MIRRORING); }
        }

        public bool Mirroring
        {
            get { return GetProperty<int>(OniCAPI.ONI_STREAM_PROPERTY_MIRRORING) == 1; }
            set { SetProperty<int>(OniCAPI.ONI_STREAM_PROPERTY_MIRRORING, value ? 1 : 0); }
        }

        public bool IsNumberOfFramesPropertySupported
        {
            get { return IsPropertySupported(OniCAPI.ONI_STREAM_PROPERTY_NUMBER_OF_FRAMES); }
        }

        public int NumberOfFrames
        {
            get { return GetProperty<int>(OniCAPI.ONI_STREAM_PROPERTY_NUMBER_OF_FRAMES); }
            set { SetProperty<int>(OniCAPI.ONI_STREAM_PROPERTY_NUMBER_OF_FRAMES, value); }
        }

        // Camera

        public bool IsAutoWhiteBalancePropertySupported
        {
            get { return IsPropertySupported(OniCAPI.ONI_STREAM_PROPERTY_AUTO_WHITE_BALANCE); }
        }

        public bool AutoWhiteBalance
        {
            get { return GetProperty<int>(OniCAPI.ONI_STREAM_PROPERTY_AUTO_WHITE_BALANCE) == 1; }
            set { SetProperty<int>(OniCAPI.ONI_STREAM_PROPERTY_AUTO_WHITE_BALANCE, value ? 1 : 0); }
        }

        public bool IsAutoExposurePropertySupported
        {
            get { return IsPropertySupported(OniCAPI.ONI_STREAM_PROPERTY_AUTO_EXPOSURE); }
        }

        public bool AutoExposure
        {
            get { return GetProperty<int>(OniCAPI.ONI_STREAM_PROPERTY_AUTO_EXPOSURE) == 1; }
            set { SetProperty<int>(OniCAPI.ONI_STREAM_PROPERTY_AUTO_EXPOSURE, value ? 1 : 0); }
        }

        public bool IsExposurePropertySupported
        {
            get { return IsPropertySupported(OniCAPI.ONI_STREAM_PROPERTY_EXPOSURE); }
        }

        public int Exposure
        {
            get { return GetProperty<int>(OniCAPI.ONI_STREAM_PROPERTY_EXPOSURE); }
            set { SetProperty<int>(OniCAPI.ONI_STREAM_PROPERTY_EXPOSURE, value); }
        }

        public bool IsGainPropertySupported
        {
            get { return IsPropertySupported(OniCAPI.ONI_STREAM_PROPERTY_GAIN); }
        }

        public int Gain
        {
            get { return GetProperty<int>(OniCAPI.ONI_STREAM_PROPERTY_GAIN); }
            set { SetProperty<int>(OniCAPI.ONI_STREAM_PROPERTY_GAIN, value); }
        }
    }
}