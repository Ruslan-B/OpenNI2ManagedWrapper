using System;

namespace OpenNI2
{
    public static class OpenNIVersionExtensions
    {
        internal static OpenNIVersion ToManaged(this OniVersion oniVersion)
        {
            return new OpenNIVersion {
                Major = oniVersion.major,
                Minor = oniVersion.minor,
                Maintenance = oniVersion.maintenance,
                Build = oniVersion.build
            };
        }
    }
}