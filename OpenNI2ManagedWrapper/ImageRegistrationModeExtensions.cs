using System;

namespace OpenNI2
{

    public static class ImageRegistrationModeExtensions
    {
        internal static ImageRegistrationMode ToManaged(this OniImageRegistrationMode oniImageRegistrationMode)
        {
            switch(oniImageRegistrationMode)
            {
                case OniImageRegistrationMode.ONI_IMAGE_REGISTRATION_OFF:
                    return ImageRegistrationMode.Off;
                case OniImageRegistrationMode.ONI_IMAGE_REGISTRATION_DEPTH_TO_COLOR:
                    return ImageRegistrationMode.DepthToColor;
                default:
                    throw new ArgumentOutOfRangeException("oniImageRegistrationMode");
            }
        }

        internal static OniImageRegistrationMode ToNative(this ImageRegistrationMode imageRegistrationMode)
        {
            switch(imageRegistrationMode)
            {
                case ImageRegistrationMode.Off:
                    return OniImageRegistrationMode.ONI_IMAGE_REGISTRATION_OFF;
                case ImageRegistrationMode.DepthToColor:
                    return OniImageRegistrationMode.ONI_IMAGE_REGISTRATION_DEPTH_TO_COLOR;
                default:
                    throw new ArgumentOutOfRangeException("imageRegistrationMode");
            }
        }
    }
}
