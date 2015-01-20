using System;

namespace OpenNI2
{
    public static class PixelFormatExtensions
    {
        public static int BytesPerPixel(this PixelFormat pixelFormat)
        {
            return OniCAPI.oniFormatBytesPerPixel(pixelFormat.ToNative());
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

        internal static OniPixelFormat ToNative(this PixelFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                case PixelFormat.Depth1MM:
                    return OniPixelFormat.ONI_PIXEL_FORMAT_DEPTH_1_MM;
                case PixelFormat.Depth100UM:
                    return OniPixelFormat.ONI_PIXEL_FORMAT_DEPTH_100_UM;
                case PixelFormat.Shift92:
                    return OniPixelFormat.ONI_PIXEL_FORMAT_SHIFT_9_2;
                case PixelFormat.Shift93:
                    return OniPixelFormat.ONI_PIXEL_FORMAT_SHIFT_9_3;
                case PixelFormat.Rgb888:
                    return OniPixelFormat.ONI_PIXEL_FORMAT_RGB888;
                case PixelFormat.Yuv422:
                    return OniPixelFormat.ONI_PIXEL_FORMAT_YUV422;
                case PixelFormat.Gray8:
                    return OniPixelFormat.ONI_PIXEL_FORMAT_GRAY8;
                case PixelFormat.Gray16:
                    return OniPixelFormat.ONI_PIXEL_FORMAT_GRAY16;
                case PixelFormat.Jpeg:
                    return OniPixelFormat.ONI_PIXEL_FORMAT_JPEG;
                case PixelFormat.Yuyv:
                    return OniPixelFormat.ONI_PIXEL_FORMAT_YUYV;
                default:
                    throw new ArgumentOutOfRangeException("pixelFormat");
            }
        }
    }
}