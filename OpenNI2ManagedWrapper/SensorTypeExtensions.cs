using System;

namespace OpenNI2
{

    public static class SensorTypeExtensions
    {
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