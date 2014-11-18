using System;

namespace OpenNI2
{
    internal unsafe static class OniCAPIExtensions
    {
        internal static void ThrowExectionIfStatusIsNotOk(this OniStatus status)
        {
            switch (status)
            {
                case OniStatus.ONI_STATUS_OK: 
                    return;
                case OniStatus.ONI_STATUS_NOT_IMPLEMENTED:
                    throw new NotImplementedException();
                case OniStatus.ONI_STATUS_NOT_SUPPORTED:
                    throw new NotSupportedException();
                case OniStatus.ONI_STATUS_ERROR: 
                case OniStatus.ONI_STATUS_BAD_PARAMETER:
                case OniStatus.ONI_STATUS_OUT_OF_FLOW:
                case OniStatus.ONI_STATUS_NO_DEVICE:
                    string message = OpenNI.GetExtendedError();
                    throw new OpenNIException(message);
                case OniStatus.ONI_STATUS_TIME_OUT:
                    throw new TimeoutException();
                default:
                    throw new ArgumentOutOfRangeException("status", string.Format("Unkown status :{0}.", status));
            }
        }

        internal static DeviceInfo ToManaged(this OniDeviceInfo oniDeviceInfo)
        {
            return new DeviceInfo
            {
                Uri = new String((sbyte*)oniDeviceInfo.uri),
                Vendor = new String((sbyte*)oniDeviceInfo.vendor),
                Name = new String((sbyte*)oniDeviceInfo.name),
                UsbVendorId = oniDeviceInfo.usbVendorId,
                UsbProductId = oniDeviceInfo.usbProductId
            };
        }

        internal static SensorType ToManaged(this OniSensorType oniSensorType)
        {
            switch (oniSensorType)
            {
                case OniSensorType.ONI_SENSOR_IR:
                    return SensorType.Infrared;
                case OniSensorType.ONI_SENSOR_COLOR:
                    return SensorType.Color;
                case OniSensorType.ONI_SENSOR_DEPTH:
                    return SensorType.Depth;
                default:
                    throw new ArgumentOutOfRangeException("oniSensorType");
            }
        }

        internal static PixelFormat ToManaged(this OniPixelFormat oniPixelFormat)
        {
            switch (oniPixelFormat)
            {
                case OniPixelFormat.ONI_PIXEL_FORMAT_DEPTH_1_MM:
                    return PixelFormat.Depth1MM;
                case OniPixelFormat.ONI_PIXEL_FORMAT_DEPTH_100_UM:
                    return PixelFormat.Depth100UM;
                case OniPixelFormat.ONI_PIXEL_FORMAT_SHIFT_9_2:
                    return PixelFormat.Shift92;
                case OniPixelFormat.ONI_PIXEL_FORMAT_SHIFT_9_3:
                    return PixelFormat.Shift93;
                case OniPixelFormat.ONI_PIXEL_FORMAT_RGB888:
                    return PixelFormat.Rgb888;
                case OniPixelFormat.ONI_PIXEL_FORMAT_YUV422:
                    return PixelFormat.Yuv422;
                case OniPixelFormat.ONI_PIXEL_FORMAT_GRAY8:
                    return PixelFormat.Gray8;
                case OniPixelFormat.ONI_PIXEL_FORMAT_GRAY16:
                    return PixelFormat.Gray16;
                case OniPixelFormat.ONI_PIXEL_FORMAT_JPEG:
                    return PixelFormat.Jpeg;
                case OniPixelFormat.ONI_PIXEL_FORMAT_YUYV:
                    return PixelFormat.Yuyv;
                default:
                    throw new ArgumentOutOfRangeException("oniPixelFormat");
            }
        }

        internal static VideoMode ToManaged(this OniVideoMode oniVideoMode)
        {
            return new VideoMode
            {
                PixelFormat = oniVideoMode.pixelFormat.ToManaged(),
                ResolutionX = oniVideoMode.resolutionX,
                ResolutionY = oniVideoMode.resolutionY,
                Fps = oniVideoMode.fps
            };
        }

        internal static SensorInfo ToManaged(this OniSensorInfo oniSensorInfo)
        {
            VideoMode[] videoModes = new VideoMode[oniSensorInfo.numSupportedVideoModes];
            for (int i = 0; i < oniSensorInfo.numSupportedVideoModes; i++)
            {
                videoModes[i] = oniSensorInfo.pSupportedVideoModes[i].ToManaged();
            }

            return new SensorInfo
            {
                SensorType = oniSensorInfo.sensorType.ToManaged(),
                IsSupported = true,
                VideoModes = videoModes
            };
        }

        internal static OniSensorType ToNative(this SensorType sensorType)
        {
            switch (sensorType)
            {
                case SensorType.Infrared:
                    return OniSensorType.ONI_SENSOR_IR;
                case SensorType.Color:
                    return OniSensorType.ONI_SENSOR_COLOR;
                case SensorType.Depth:
                    return OniSensorType.ONI_SENSOR_DEPTH;
                default:
                    throw new ArgumentOutOfRangeException("sensorType");
            }
        }
    }
}

