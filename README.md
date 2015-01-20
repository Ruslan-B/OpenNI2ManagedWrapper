OpenNI2 managed wrapper for .NET/Mono
================================

Based on OpenNI2 version from Occipital. 
https://github.com/occipital/openni2


##Goal
Provide near native api support for PS1080 sensor from Occipital.
http://structure.io


##Impementation details
Internaly wrapper using auto generated unmanaged OpenNI CAPI. 
```OpenNI2.XmlToCSharp``` project transforms XML to C#. 
Original CAPI definitions compiled/generated to XML by (gccxml)[https://github.com/gccxml/gccxml].


##Work in progress:
- PS1080 specific properties support;
- Device ONI_DEVICE_PROPERTY_IMAGE_REGISTRATION property support;
- SensorStream ONI_STREAM_PROPERTY_CROPPING property support;
- API methods to cover:
```csharp
// device

internal static extern int oniDeviceIsImageRegistrationModeSupported(_OniDevice* device, OniImageRegistrationMode mode);

// stream
internal static extern int oniStreamIsCommandSupported(_OniStream* stream, int commandId);
internal static extern OniStatus oniStreamInvoke(_OniStream* stream, int commandId, byte* data, int dataSize);

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
internal static extern OniStatus oniSetLogMinSeverity(int nMinSeverity);
internal static extern OniStatus oniSetLogConsoleOutput(int bConsoleOutput);
internal static extern OniStatus oniGetLogFileName(byte* strFileName, int nBufferSize);
internal static extern OniStatus oniSetLogOutputFolder(byte* strOutputFolder);
internal static extern OniStatus oniSetLogFileOutput(int bFileOutput);

// callbacks
internal static extern OniStatus oniRegisterDeviceCallbacks(OniDeviceCallbacks pCallbacks, byte* pCookie, OniCallbackHandleImpl** pHandle);
internal static extern byte oniUnregisterDeviceCallbacks(OniCallbackHandleImpl* handle);
```