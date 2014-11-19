namespace OpenNI2
{
    public struct VideoMode
    {
        public PixelFormat PixelFormat { get; internal set; }

        public int ResolutionX { get; internal set; }

        public int ResolutionY { get; internal set; }

        public int Fps { get; internal set; }
    }
}