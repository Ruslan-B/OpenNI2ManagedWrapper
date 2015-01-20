namespace OpenNI2
{

    public static class VideoModeExtensions
    {
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

        internal static OniVideoMode ToNative(this VideoMode videoMode)
        {
            return new OniVideoMode
            {
                pixelFormat = videoMode.PixelFormat.ToNative(),
                resolutionX = videoMode.ResolutionX,
                resolutionY = videoMode.ResolutionY,
                fps = videoMode.Fps
            };
        }
    }
}