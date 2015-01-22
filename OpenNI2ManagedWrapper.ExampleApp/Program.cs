using System;
using System.IO;
using System.Linq;
using OpenNI2.ExampleApp.Properties;

namespace OpenNI2.ExampleApp
{
    internal static class MainClass
    {
        public static void Main(string[] args)
        {
            InteropHelper.RegisterLibrariesSearchPath(Settings.Default.OpenNIPathOnWindows);

            Console.WriteLine("Runnung in {0}-bit mode.", Environment.Is64BitProcess ? "64" : "32");

            OpenNI.Initialize();

            OpenNIVersion openNIVersion = OpenNI.GetVersion();
            Console.WriteLine("OpenNI version: {0}.{1}.{2}.{3}.", openNIVersion.Major, openNIVersion.Minor, openNIVersion.Maintenance, openNIVersion.Build);
            Console.WriteLine();

            //OpenNI.SetLogMinSeverity(0);
            //OpenNI.SetLogConsoleOutput(true);
            //OpenNI.SetLogFileOutput(true);   
            //Console.WriteLine("Log file path: {0}", OpenNI.GetLogFileName());

            DeviceInfo[] devices = OpenNI.GetDevices();
            Console.WriteLine("Found {0} device(s):", devices.Length);
            devices.ToList().ForEach(x => Console.WriteLine("{0} from {1} @ {2} with usb id {3}:{4}.", x.Name, x.Vendor, x.Uri, x.UsbVendorId, x.UsbProductId));
            Console.WriteLine();

            if (devices.Length == 0) return;

            // open default device
            using (Device device = Device.Open())
            {
                DeviceInfo deviceInfo = device.GetDeviceInfo();
                Console.WriteLine("Device {0} @ {1} was successfully opened.", deviceInfo.Name, deviceInfo.Uri);
                Console.WriteLine();

                if (device.IsDriverVersionPropertySupported)
                {
                    OpenNIVersion driverVersion = device.DriverVersion;
                    Console.WriteLine("Driver version: {0}.{1}.{2}.{3}.", driverVersion.Major, driverVersion.Minor, driverVersion.Maintenance, driverVersion.Build);
                    Console.WriteLine("Hardware version: {0}.", device.HardwareVersion);
                    Console.WriteLine("Serial number: {0}.", device.SerialNumber);
                    Console.WriteLine();
                }

                SensorInfo infraredSensorInfo = device.GetSensorInfo(SensorType.Infrared);
                DescribeSensor(infraredSensorInfo);
                Console.WriteLine();

                SensorInfo colorSensorInfo = device.GetSensorInfo(SensorType.Color);
                DescribeSensor(colorSensorInfo);
                Console.WriteLine();

                SensorInfo depthSensorInfo = device.GetSensorInfo(SensorType.Depth);
                DescribeSensor(depthSensorInfo);
                Console.WriteLine();

                using (SensorStream stream = device.CreateStream(SensorType.Depth))
                {
                    SensorInfo streamSensorInfo = stream.GetSensorInfo();
                    DescribeVideoModes(streamSensorInfo);
                    Console.WriteLine();

                    VideoMode videoMode = stream.VideoMode;

                    float hFov = stream.IsHorizontalFovPropertySupported ? stream.HorizontalFov : float.NaN;
                    float vFov = stream.IsVerticalFovPropertySupported ? stream.VerticalFov : float.NaN;
                    int minValue = stream.IsMinValuePropertySupported ? stream.MinValue : -1;
                    int maxValue = stream.IsMaxValuePropertySupported ? stream.MaxValue : -1;

                    Console.WriteLine("Stream properties:");
                    float rtdK = (float)(180.0 / Math.PI );
                    Console.WriteLine("Horizontal {0:0.0} (deg) and vertical {1:0.0} (deg) FOV.",  hFov * rtdK, vFov * rtdK);
                    //Console.WriteLine("Min {0} and max {1} values.", minValue, maxValue);

                    stream.Start();

                    using (SensorFrame frame = stream.ReadFrame())
                    {
                        Console.WriteLine("Video frame acquired:");
                        DescribeFrame(frame);

                        // write frame
                        using (Stream fileStream = File.Open("sensor.frame", FileMode.Create))
                        using (BinaryWriter writer = new BinaryWriter(fileStream))
                        {
                            if (false)
                            {
                                // stream properties
                                writer.Write(hFov);
                                writer.Write(vFov);
                                writer.Write(minValue);
                                writer.Write(maxValue);
                                writer.Write(videoMode.PixelFormat.BytesPerPixel());

                                // frame properties
                                writer.Write(frame.CroppingEnabled);
                                writer.Write(frame.CropOriginX);
                                writer.Write(frame.CropOriginY);
                                writer.Write(frame.Width);
                                writer.Write(frame.Height);
                                writer.Write(frame.Stride);
                                writer.Write(frame.Data.Size);
                            }

                            // data
                            using (Stream dataStream = frame.Data.CreateStream())
                            {
                                dataStream.CopyTo(fileStream);
                            }

                            fileStream.Flush();
                        }
                    }

                    stream.Stop();
                }
            }
            OpenNI.Shutdown();
        }

        private static void DescribeVideoModes(SensorInfo sensorInfo)
        {
            foreach (VideoMode videoMode in sensorInfo.VideoModes)
            {
                Console.WriteLine("{0} {1}x{2} @ {3} fps.", videoMode.PixelFormat, videoMode.ResolutionX, videoMode.ResolutionY, videoMode.Fps);
            }
        }

        public static void DescribeSensor(SensorInfo sensorInfo)
        {
            if (sensorInfo.IsSupported)
            {
                Console.WriteLine("{0} sensor has {1} mode(s).", sensorInfo.SensorType, sensorInfo.VideoModes.Length);
            }
            else
            {
                Console.WriteLine("{0} sensor is not supported.", sensorInfo.SensorType);
            }
        }

        public static void DescribeFrame(SensorFrame frame)
        {
            Console.WriteLine("Sensor type: {0}", frame.SensorType);
            Console.WriteLine("Timestamp: {0}", frame.Timestamp);
            Console.WriteLine("Index: {0}", frame.FrameIndex);
            Console.WriteLine("Video Mode: {0}, {1} byte(s) pps, {2}x{3} @ {4} fps.", 
                frame.VideoMode.PixelFormat, 
                frame.VideoMode.PixelFormat.BytesPerPixel(),
                frame.VideoMode.ResolutionX, frame.VideoMode.ResolutionY,
                frame.VideoMode.Fps);
            Console.WriteLine("Clipping: {0}", frame.CroppingEnabled);
            Console.WriteLine("Crop: x - {0}, y - {1}", frame.CropOriginX, frame.CropOriginY);
            Console.WriteLine("Stride: {0}", frame.Stride);
            Console.WriteLine("Data size: {0} byte(s)", frame.Data.Size);
        }
    }
}