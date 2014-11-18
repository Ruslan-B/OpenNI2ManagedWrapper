using System;

namespace OpenNI2
{
    public struct DeviceInfo
    {
        public string Uri { get; internal set; }

        public string Vendor { get; internal set; }

        public string Name { get; internal set; }

        public ushort UsbVendorId { get; internal set; }

        public ushort UsbProductId { get; internal set; }
    }
}