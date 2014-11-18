﻿using System;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace OpenNI2.ExampleApp
{
    internal class MainClass
    {
        static void DescribeVideoModes(SensorInfo sensorInfo)
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

        public static void DescribeFrame(VideoFrame frame)
        {
            Console.WriteLine("Sensor type: {0}", frame.SensorType);
            Console.WriteLine("Timestamp: {0}", frame.Timestamp);
            Console.WriteLine("Index: {0}", frame.FrameIndex);
            Console.WriteLine("Video Mode: {0} {1}x{2} @ {3} fps.", frame.VideoMode.PixelFormat, frame.VideoMode.ResolutionX, frame.VideoMode.ResolutionY, frame.VideoMode.Fps);
            Console.WriteLine("Clipping: {0}", frame.CroppingEnabled);
            Console.WriteLine("Crop: x - {0}, y - {1}", frame.CropOriginX, frame.CropOriginY);
            Console.WriteLine("Stride: {0}", frame.Stride);
            Console.WriteLine("Data size: {0} byte(s)", frame.Data.Size);
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Runnung in {0}-bit mode.", Environment.Is64BitProcess ? "64" : "32");
            Console.WriteLine();

            OpenNI.Initialize();

            DeviceInfo[] devices = OpenNI.GetDevices();
            Console.WriteLine("Found {0} device(s):", devices.Length);
            devices.ToList().ForEach(x => Console.WriteLine("{0} from {1} @ {2} with usb id {3}:{4}.", x.Name, x.Vendor, x.Uri, x.UsbVendorId, x.UsbProductId));
            Console.WriteLine();

            using (Device device = Device.Open(devices.First().Uri))
            {
                DeviceInfo deviceInfo = device.GetDeviceInfo();
                Console.WriteLine("Device {0} @ {1} was successfully opened.", deviceInfo.Name, deviceInfo.Uri);
                Console.WriteLine();

                SensorInfo infraredSensorInfo = device.GetSensorInfo(SensorType.Infrared);
                DescribeSensor(infraredSensorInfo);
                Console.WriteLine();

                SensorInfo colorSensorInfo = device.GetSensorInfo(SensorType.Color);
                DescribeSensor(colorSensorInfo);
                Console.WriteLine();

                SensorInfo depthSensorInfo = device.GetSensorInfo(SensorType.Depth);
                DescribeSensor(depthSensorInfo);
                Console.WriteLine();

                using (VideoStream stream = device.CreateStream(SensorType.Depth))
                {
                    SensorInfo streamSensorInfo = stream.GetSensorInfo();
                    DescribeVideoModes(streamSensorInfo);
                    Console.WriteLine();

                    stream.Start();

                    using (VideoFrame frame = stream.ReadFrame())
                    using (Stream dataStream = frame.Data.CreateStream())
                    using (Stream fileStream = File.Open("frame.data", FileMode.Create))
                    {
                        Console.WriteLine("Video frame acquired:");
                        DescribeFrame(frame);

                        dataStream.CopyTo(fileStream);
                        fileStream.Flush();
                    }

                    stream.Stop();
                }
            }

            OpenNI.Shutdown();
        }
    }
}