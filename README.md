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
- SensorStream ONI_STREAM_PROPERTY_CROPPING property support;
- API methods to cover:
```csharp

// stream
internal static extern OniStatus oniStreamRegisterNewFrameCallback(_OniStream* stream, OniNewFrameCallback handler, byte* pCookie, OniCallbackHandleImpl** pHandle);
internal static extern byte oniStreamUnregisterNewFrameCallback(_OniStream* stream, OniCallbackHandleImpl* handle);
internal static extern OniStatus oniStreamSetFrameBuffersAllocator(_OniStream* stream, OniFrameAllocBufferCallback alloc, OniFrameFreeBufferCallback free, byte* pCookie);

// frame
???internal static extern byte oniFrameAddRef(OniFrame* pFrame);
???internal static extern byte oniFrameRelease(OniFrame* pFrame);

// callbacks
internal static extern OniStatus oniRegisterDeviceCallbacks(OniDeviceCallbacks pCallbacks, byte* pCookie, OniCallbackHandleImpl** pHandle);
internal static extern byte oniUnregisterDeviceCallbacks(OniCallbackHandleImpl* handle);
```