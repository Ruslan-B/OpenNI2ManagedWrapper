namespace OpenNI2
{
    public static class PixelFormatExtensions
    {
        public static int BytesPerPixel(this PixelFormat pixelFormat)
        {
            return OniCAPI.oniFormatBytesPerPixel(pixelFormat.ToNative());
        }
    }
}