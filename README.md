OpenNI2 managed wrapper for .NET
================================

Based on https://github.com/occipital/openni2

Native types has been auto generated with help of gccxml(https://github.com/gccxml/gccxml).
Using OniCAPI.h include file as the entry point.

TODO:

API methods to cover:

// device
internal static extern int oniDeviceIsCommandSupported(_OniDevice* device, int commandId);
internal static extern OniStatus oniDeviceInvoke(_OniDevice* device, int commandId, byte* data, int dataSize);

internal static extern int oniDeviceIsPropertySupported(_OniDevice* device, int propertyId);
internal static extern OniStatus oniDeviceGetProperty(_OniDevice* device, int propertyId, byte* data, int* pDataSize);
internal static extern OniStatus oniDeviceSetProperty(_OniDevice* device, int propertyId, byte* data, int dataSize);

internal static extern OniStatus oniDeviceEnableDepthColorSync(_OniDevice* device);
internal static extern int oniDeviceGetDepthColorSyncEnabled(_OniDevice* device);
internal static extern byte oniDeviceDisableDepthColorSync(_OniDevice* device);

internal static extern int oniDeviceIsImageRegistrationModeSupported(_OniDevice* device, OniImageRegistrationMode mode);
???internal static extern OniStatus oniDeviceOpenEx(byte* uri, byte* mode, _OniDevice** pDevice);


// stream
internal static extern int oniStreamIsPropertySupported(_OniStream* stream, int propertyId);
internal static extern OniStatus oniStreamSetProperty(_OniStream* stream, int propertyId, byte* data, int dataSize);
internal static extern OniStatus oniStreamGetProperty(_OniStream* stream, int propertyId, byte* data, int* pDataSize);

internal static extern int oniStreamIsCommandSupported(_OniStream* stream, int commandId);
internal static extern OniStatus oniStreamInvoke(_OniStream* stream, int commandId, byte* data, int dataSize);

internal static extern OniStatus oniCoordinateConverterDepthToWorld(_OniStream* depthStream, float depthX, float depthY, float depthZ, float* pWorldX, float* pWorldY, float* pWorldZ);
internal static extern OniStatus oniCoordinateConverterDepthToColor(_OniStream* depthStream, _OniStream* colorStream, int depthX, int depthY, ushort depthZ, int* pColorX, int* pColorY);
internal static extern OniStatus oniCoordinateConverterWorldToDepth(_OniStream* depthStream, float worldX, float worldY, float worldZ, float* pDepthX, float* pDepthY, float* pDepthZ);

internal static extern OniStatus oniStreamRegisterNewFrameCallback(_OniStream* stream, OniNewFrameCallback handler, byte* pCookie, OniCallbackHandleImpl** pHandle);
internal static extern byte oniStreamUnregisterNewFrameCallback(_OniStream* stream, OniCallbackHandleImpl* handle);
internal static extern OniStatus oniStreamSetFrameBuffersAllocator(_OniStream* stream, OniFrameAllocBufferCallback alloc, OniFrameFreeBufferCallback free, byte* pCookie);

internal static extern OniStatus oniWaitForAnyStream(_OniStream** pStreams, int numStreams, int* pStreamIndex, int timeout);

// frame
???internal static extern byte oniFrameAddRef(OniFrame* pFrame);
???internal static extern byte oniFrameRelease(OniFrame* pFrame);


// recorder
internal static extern OniStatus oniCreateRecorder(byte* fileName, _OniRecorder** pRecorder);
internal static extern OniStatus oniRecorderStart(_OniRecorder* recorder);
internal static extern OniStatus oniRecorderAttachStream(_OniRecorder* recorder, _OniStream* stream, int allowLossyCompression);
internal static extern OniStatus oniRecorderDestroy(_OniRecorder** pRecorder);
internal static extern byte oniRecorderStop(_OniRecorder* recorder);

// generic
internal static extern OniVersion oniGetVersion();
internal static extern OniStatus oniSetLogMinSeverity(int nMinSeverity);
internal static extern OniStatus oniSetLogConsoleOutput(int bConsoleOutput);
internal static extern OniStatus oniGetLogFileName(byte* strFileName, int nBufferSize);
internal static extern OniStatus oniSetLogOutputFolder(byte* strOutputFolder);
internal static extern OniStatus oniSetLogFileOutput(int bFileOutput);

// pixel format
internal static extern int oniFormatBytesPerPixel(OniPixelFormat format);

internal static extern OniStatus oniRegisterDeviceCallbacks(OniDeviceCallbacks pCallbacks, byte* pCookie, OniCallbackHandleImpl** pHandle);
internal static extern byte oniUnregisterDeviceCallbacks(OniCallbackHandleImpl* handle);
