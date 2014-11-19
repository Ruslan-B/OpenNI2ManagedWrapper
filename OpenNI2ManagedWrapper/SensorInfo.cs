namespace OpenNI2
{
    public struct SensorInfo
    {
        public SensorType SensorType { get; internal set; }

        public bool IsSupported { get; internal set; }

        public VideoMode[] VideoModes { get; internal set; }
    }
}