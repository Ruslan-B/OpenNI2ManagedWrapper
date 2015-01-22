using System.Runtime.InteropServices;

namespace OpenNI2
{
    internal enum OniImageRegistrationMode
    {
        ONI_IMAGE_REGISTRATION_OFF = 0,
        ONI_IMAGE_REGISTRATION_DEPTH_TO_COLOR = 1,
    }
    
    internal enum OniPixelFormat
    {
        ONI_PIXEL_FORMAT_DEPTH_1_MM = 100,
        ONI_PIXEL_FORMAT_DEPTH_100_UM = 101,
        ONI_PIXEL_FORMAT_SHIFT_9_2 = 102,
        ONI_PIXEL_FORMAT_SHIFT_9_3 = 103,
        ONI_PIXEL_FORMAT_RGB888 = 200,
        ONI_PIXEL_FORMAT_YUV422 = 201,
        ONI_PIXEL_FORMAT_GRAY8 = 202,
        ONI_PIXEL_FORMAT_GRAY16 = 203,
        ONI_PIXEL_FORMAT_JPEG = 204,
        ONI_PIXEL_FORMAT_YUYV = 205,
    }
    
    internal enum OniDeviceState
    {
        ONI_DEVICE_STATE_OK = 0,
        ONI_DEVICE_STATE_ERROR = 1,
        ONI_DEVICE_STATE_NOT_READY = 2,
        ONI_DEVICE_STATE_EOF = 3,
    }
    
    internal enum OniStatus
    {
        ONI_STATUS_OK = 0,
        ONI_STATUS_ERROR = 1,
        ONI_STATUS_NOT_IMPLEMENTED = 2,
        ONI_STATUS_NOT_SUPPORTED = 3,
        ONI_STATUS_BAD_PARAMETER = 4,
        ONI_STATUS_OUT_OF_FLOW = 5,
        ONI_STATUS_NO_DEVICE = 6,
        ONI_STATUS_TIME_OUT = 102,
    }
    
    internal enum OniSensorType
    {
        ONI_SENSOR_IR = 1,
        ONI_SENSOR_COLOR = 2,
        ONI_SENSOR_DEPTH = 3,
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack = 4)]
    internal unsafe struct OniVideoMode
    {
        internal OniPixelFormat pixelFormat;
        internal int resolutionX;
        internal int resolutionY;
        internal int fps;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack = 4)]
    internal unsafe struct OniFrame
    {
        internal int dataSize;
        internal void* data;
        internal OniSensorType sensorType;
        internal ulong timestamp;
        internal int frameIndex;
        internal int width;
        internal int height;
        internal OniVideoMode videoMode;
        internal int croppingEnabled;
        internal int cropOriginX;
        internal int cropOriginY;
        internal int stride;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack = 2)]
    internal unsafe struct OniDeviceInfo
    {
        internal fixed byte uri [256];
        internal fixed byte vendor [256];
        internal fixed byte name [256];
        internal ushort usbVendorId;
        internal ushort usbProductId;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack = 4)]
    internal unsafe struct OniVersion
    {
        internal int major;
        internal int minor;
        internal int maintenance;
        internal int build;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack = 4)]
    internal unsafe struct OniSensorInfo
    {
        internal OniSensorType sensorType;
        internal int numSupportedVideoModes;
        internal OniVideoMode* pSupportedVideoModes;
    }
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    internal unsafe delegate void OniDeviceInfoCallback (OniDeviceInfo* p0, void* p1);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    internal unsafe delegate void OniDeviceStateCallback (OniDeviceInfo* p0, OniDeviceState p1, void* p2);
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack = 4)]
    internal unsafe struct OniDeviceCallbacks
    {
        internal OniDeviceInfoCallback deviceConnected;
        internal OniDeviceInfoCallback deviceDisconnected;
        internal OniDeviceStateCallback deviceStateChanged;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack = 4)]
    internal unsafe struct OniCropping
    {
        internal int enabled;
        internal int originX;
        internal int originY;
        internal int width;
        internal int height;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack = 1)]
    internal unsafe struct OniYUV422DoublePixel
    {
        internal byte u;
        internal byte y1;
        internal byte v;
        internal byte y2;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack = 1)]
    internal unsafe struct _OniRecorder
    {
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack = 1)]
    internal unsafe struct OniRGB888Pixel
    {
        internal byte r;
        internal byte g;
        internal byte b;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack = 1)]
    internal unsafe struct _OniStream
    {
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack = 4)]
    internal unsafe struct OniSeek
    {
        internal int frameIndex;
        internal _OniStream* stream;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack = 1)]
    internal unsafe struct OniCallbackHandleImpl
    {
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack = 1)]
    internal unsafe struct _OniDevice
    {
    }
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    internal unsafe delegate void* OniFrameAllocBufferCallback (int p0, void* p1);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    internal unsafe delegate void OniFrameFreeBufferCallback (void* p0, void* p1);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    internal unsafe delegate void OniNewFrameCallback (_OniStream* p0, void* p1);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    internal unsafe delegate void OniGeneralCallback (void* p0);
    
    internal static unsafe class OniCAPI
    {
        internal const int ONI_TIMEOUT_NONE = 0;
        internal const int ONI_TIMEOUT_FOREVER = -1;
        
        internal const int ONI_DEVICE_PROPERTY_FIRMWARE_VERSION = 0;
        internal const int ONI_DEVICE_PROPERTY_DRIVER_VERSION = 1;
        internal const int ONI_DEVICE_PROPERTY_HARDWARE_VERSION = 2;
        internal const int ONI_DEVICE_PROPERTY_SERIAL_NUMBER = 3;
        internal const int ONI_DEVICE_PROPERTY_ERROR_STATE = 4;
        internal const int ONI_DEVICE_PROPERTY_IMAGE_REGISTRATION = 5;
        internal const int ONI_DEVICE_PROPERTY_PLAYBACK_SPEED = 100;
        internal const int ONI_DEVICE_PROPERTY_PLAYBACK_REPEAT_ENABLED = 101;
        
        internal const int ONI_DEVICE_COMMAND_SEEK = 1;
        
        internal const int ONI_STREAM_PROPERTY_CROPPING = 0;
        internal const int ONI_STREAM_PROPERTY_HORIZONTAL_FOV = 1;
        internal const int ONI_STREAM_PROPERTY_VERTICAL_FOV = 2;
        internal const int ONI_STREAM_PROPERTY_VIDEO_MODE = 3;
        internal const int ONI_STREAM_PROPERTY_MAX_VALUE = 4;
        internal const int ONI_STREAM_PROPERTY_MIN_VALUE = 5;
        internal const int ONI_STREAM_PROPERTY_STRIDE = 6;
        internal const int ONI_STREAM_PROPERTY_MIRRORING = 7;
        internal const int ONI_STREAM_PROPERTY_NUMBER_OF_FRAMES = 8;
        internal const int ONI_STREAM_PROPERTY_AUTO_WHITE_BALANCE = 100;
        internal const int ONI_STREAM_PROPERTY_AUTO_EXPOSURE = 101;
        internal const int ONI_STREAM_PROPERTY_EXPOSURE = 102;
        internal const int ONI_STREAM_PROPERTY_GAIN = 103;
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern void oniStreamStop(_OniStream* stream);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniDeviceEnableDepthColorSync(_OniDevice* device);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniStreamSetFrameBuffersAllocator(_OniStream* stream, OniFrameAllocBufferCallback alloc, OniFrameFreeBufferCallback free, void* pCookie);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern int oniDeviceIsImageRegistrationModeSupported(_OniDevice* device, OniImageRegistrationMode mode);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniDeviceCreateStream(_OniDevice* device, OniSensorType sensorType, _OniStream** pStream);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniGetDeviceList(OniDeviceInfo** pDevices, int* pNumDevices);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern void oniShutdown();
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern void oniFrameAddRef(OniFrame* pFrame);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern int oniDeviceIsCommandSupported(_OniDevice* device, int commandId);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern byte* oniGetExtendedError();
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniSensorInfo* oniDeviceGetSensorInfo(_OniDevice* device, OniSensorType sensorType);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniReleaseDeviceList(OniDeviceInfo* pDevices);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniDeviceInvoke(_OniDevice* device, int commandId, void* data, int dataSize);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniCoordinateConverterDepthToWorld(_OniStream* depthStream, float depthX, float depthY, float depthZ, float* pWorldX, float* pWorldY, float* pWorldZ);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern void oniDeviceDisableDepthColorSync(_OniDevice* device);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniStreamSetProperty(_OniStream* stream, int propertyId, void* data, int dataSize);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniVersion oniGetVersion();
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniRecorderStart(_OniRecorder* recorder);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniRegisterDeviceCallbacks(OniDeviceCallbacks pCallbacks, void* pCookie, OniCallbackHandleImpl** pHandle);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniSetLogConsoleOutput(int bConsoleOutput);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniDeviceGetProperty(_OniDevice* device, int propertyId, void* data, int* pDataSize);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern int oniStreamIsCommandSupported(_OniStream* stream, int commandId);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniCreateRecorder(byte* fileName, _OniRecorder** pRecorder);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniSetLogMinSeverity(int nMinSeverity);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern void oniStreamUnregisterNewFrameCallback(_OniStream* stream, OniCallbackHandleImpl* handle);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniDeviceOpen(byte* uri, _OniDevice** pDevice);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern void oniFrameRelease(OniFrame* pFrame);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniGetLogFileName(byte* strFileName, int nBufferSize);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniCoordinateConverterDepthToColor(_OniStream* depthStream, _OniStream* colorStream, int depthX, int depthY, ushort depthZ, int* pColorX, int* pColorY);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniRecorderAttachStream(_OniRecorder* recorder, _OniStream* stream, int allowLossyCompression);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniInitialize(int apiVersion);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniSetLogOutputFolder(byte* strOutputFolder);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniStreamReadFrame(_OniStream* stream, OniFrame** pFrame);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniStreamGetProperty(_OniStream* stream, int propertyId, void* data, int* pDataSize);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniDeviceSetProperty(_OniDevice* device, int propertyId, void* data, int dataSize);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniRecorderDestroy(_OniRecorder** pRecorder);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniDeviceGetInfo(_OniDevice* device, OniDeviceInfo* pInfo);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniSensorInfo* oniStreamGetSensorInfo(_OniStream* stream);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniDeviceClose(_OniDevice* device);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern void oniUnregisterDeviceCallbacks(OniCallbackHandleImpl* handle);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern void oniRecorderStop(_OniRecorder* recorder);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniSetLogFileOutput(int bFileOutput);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniStreamInvoke(_OniStream* stream, int commandId, void* data, int dataSize);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern int oniFormatBytesPerPixel(OniPixelFormat format);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniCoordinateConverterWorldToDepth(_OniStream* depthStream, float worldX, float worldY, float worldZ, float* pDepthX, float* pDepthY, float* pDepthZ);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniStreamStart(_OniStream* stream);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern int oniDeviceIsPropertySupported(_OniDevice* device, int propertyId);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniStreamRegisterNewFrameCallback(_OniStream* stream, OniNewFrameCallback handler, void* pCookie, OniCallbackHandleImpl** pHandle);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniDeviceOpenEx(byte* uri, byte* mode, _OniDevice** pDevice);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern OniStatus oniWaitForAnyStream(_OniStream** pStreams, int numStreams, int* pStreamIndex, int timeout);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern int oniDeviceGetDepthColorSyncEnabled(_OniDevice* device);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern int oniStreamIsPropertySupported(_OniStream* stream, int propertyId);
        
        [DllImport(@"OpenNI2", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        internal static extern void oniStreamDestroy(_OniStream* stream);
        
    }
}
