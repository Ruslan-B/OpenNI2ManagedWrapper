using System.Runtime.InteropServices;

namespace OpenNI2
{
    internal enum XnSensorVer
    {
        XN_SENSOR_VER_UNKNOWN = 0,
        XN_SENSOR_VER_2_0 = 1,
        XN_SENSOR_VER_3_0 = 2,
        XN_SENSOR_VER_4_0 = 3,
        XN_SENSOR_VER_5_0 = 4,
    }
    
    internal enum XnFlashFileType
    {
        XnFlashFileTypeFileTable = 0,
        XnFlashFileTypeScratchFile = 1,
        XnFlashFileTypeBootSector = 2,
        XnFlashFileTypeBootManager = 3,
        XnFlashFileTypeCodeDownloader = 4,
        XnFlashFileTypeMonitor = 5,
        XnFlashFileTypeApplication = 6,
        XnFlashFileTypeFixedParams = 7,
        XnFlashFileTypeDescriptors = 8,
        XnFlashFileTypeDefaultParams = 9,
        XnFlashFileTypeImageCmos = 10,
        XnFlashFileTypeDepthCmos = 11,
        XnFlashFileTypeAlgorithmParams = 12,
        XnFlashFileTypeReferenceQVGA = 13,
        XnFlashFileTypeReferenceVGA = 14,
        XnFlashFileTypeMaintenance = 15,
        XnFlashFileTypeDebugParams = 16,
        XnFlashFileTypePrimeProcessor = 17,
        XnFlashFileTypeGainControl = 18,
        XnFlashFileTypeRegistartionParams = 19,
        XnFlashFileTypeIDParams = 20,
        XnFlashFileTypeSensorTECParams = 21,
        XnFlashFileTypeSensorAPCParams = 22,
        XnFlashFileTypeSensorProjectorFaultParams = 23,
        XnFlashFileTypeProductionFile = 24,
        XnFlashFileTypeUpgradeInProgress = 25,
        XnFlashFileTypeWavelengthCorrection = 26,
        XnFlashFileTypeGMCReferenceOffset = 27,
        XnFlashFileTypeSensorNESAParams = 28,
        XnFlashFileTypeSensorFault = 29,
        XnFlashFileTypeVendorData = 30,
    }
    
    internal enum XnChipVer
    {
        XN_SENSOR_CHIP_VER_UNKNOWN = 0,
        XN_SENSOR_CHIP_VER_PS1000 = 1,
        XN_SENSOR_CHIP_VER_PS1080 = 2,
        XN_SENSOR_CHIP_VER_PS1080A6 = 3,
    }
    
    internal enum XnCMOSType
    {
        XN_CMOS_TYPE_IMAGE = 0,
        XN_CMOS_TYPE_DEPTH = 1,
        XN_CMOS_COUNT = 2,
    }
    
    internal enum XnIOImageFormats
    {
        XN_IO_IMAGE_FORMAT_BAYER = 0,
        XN_IO_IMAGE_FORMAT_YUV422 = 1,
        XN_IO_IMAGE_FORMAT_JPEG = 2,
        XN_IO_IMAGE_FORMAT_JPEG_420 = 3,
        XN_IO_IMAGE_FORMAT_JPEG_MONO = 4,
        XN_IO_IMAGE_FORMAT_UNCOMPRESSED_YUV422 = 5,
        XN_IO_IMAGE_FORMAT_UNCOMPRESSED_BAYER = 6,
        XN_IO_IMAGE_FORMAT_UNCOMPRESSED_YUYV = 7,
    }
    
    internal enum XnIODepthFormats
    {
        XN_IO_DEPTH_FORMAT_UNCOMPRESSED_16_BIT = 0,
        XN_IO_DEPTH_FORMAT_COMPRESSED_PS = 1,
        XN_IO_DEPTH_FORMAT_UNCOMPRESSED_10_BIT = 2,
        XN_IO_DEPTH_FORMAT_UNCOMPRESSED_11_BIT = 3,
        XN_IO_DEPTH_FORMAT_UNCOMPRESSED_12_BIT = 4,
    }
    
    internal enum XnParamResetType
    {
        XN_RESET_TYPE_POWER = 0,
        XN_RESET_TYPE_SOFT = 1,
        XN_RESET_TYPE_SOFT_FIRST = 2,
    }
    
    internal enum XnLogFilter
    {
        XnLogFilterDebug = 1,
        XnLogFilterInfo = 2,
        XnLogFilterError = 4,
        XnLogFilterProtocol = 8,
        XnLogFilterAssert = 16,
        XnLogFilterConfig = 32,
        XnLogFilterFrameSync = 64,
        XnLogFilterAGC = 128,
        XnLogFilterTelems = 256,
        XnLogFilterAll = 65535,
    }
    
    internal enum XnFilePossibleAttributes
    {
        XnFileAttributeReadOnly = 32768,
    }
    
    internal enum XnFWVer
    {
        XN_SENSOR_FW_VER_UNKNOWN = 0,
        XN_SENSOR_FW_VER_0_17 = 1,
        XN_SENSOR_FW_VER_1_1 = 2,
        XN_SENSOR_FW_VER_1_2 = 3,
        XN_SENSOR_FW_VER_3_0 = 4,
        XN_SENSOR_FW_VER_4_0 = 5,
        XN_SENSOR_FW_VER_5_0 = 6,
        XN_SENSOR_FW_VER_5_1 = 7,
        XN_SENSOR_FW_VER_5_2 = 8,
        XN_SENSOR_FW_VER_5_3 = 9,
        XN_SENSOR_FW_VER_5_4 = 10,
        XN_SENSOR_FW_VER_5_5 = 11,
        XN_SENSOR_FW_VER_5_6 = 12,
        XN_SENSOR_FW_VER_5_7 = 13,
        XN_SENSOR_FW_VER_5_8 = 14,
    }
    
    internal enum XnHWVer
    {
        XN_SENSOR_HW_VER_UNKNOWN = 0,
        XN_SENSOR_HW_VER_FPDB_10 = 1,
        XN_SENSOR_HW_VER_CDB_10 = 2,
        XN_SENSOR_HW_VER_RD_3 = 3,
        XN_SENSOR_HW_VER_RD_5 = 4,
        XN_SENSOR_HW_VER_RD1081 = 5,
        XN_SENSOR_HW_VER_RD1082 = 6,
        XN_SENSOR_HW_VER_RD109 = 7,
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal unsafe struct XnFlashFile
    {
        internal ushort nId;
        internal ushort nType;
        internal uint nVersion;
        internal uint nOffset;
        internal uint nSize;
        internal ushort nCrc;
        internal ushort nAttributes;
        internal ushort nReserve;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal unsafe struct XnFlashFileList
    {
        internal XnFlashFile* pFiles;
        internal ushort nFiles;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal unsafe struct XnFileAttributes
    {
        internal ushort nId;
        internal ushort nAttribs;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal unsafe struct XnInnerParamData
    {
        internal ushort nParam;
        internal ushort nValue;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal unsafe struct XnDepthAGCBin
    {
        internal ushort nBin;
        internal ushort nMin;
        internal ushort nMax;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal unsafe struct XnTecData
    {
        internal ushort m_SetPointVoltage;
        internal ushort m_CompensationVoltage;
        internal ushort m_TecDutyCycle;
        internal ushort m_HeatMode;
        internal int m_ProportionalError;
        internal int m_IntegralError;
        internal int m_DerivativeError;
        internal ushort m_ScanMode;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal unsafe struct XnCmosBlankingTime
    {
        internal XnCMOSType nCmosID;
        internal float nTimeInMilliseconds;
        internal ushort nNumberOfFrames;
    }

    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal unsafe struct XnParamFileData
    {
        internal uint nOffset;
        internal byte* strFileName;
        internal ushort nAttributes;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal unsafe struct XnControlProcessingData
    {
        internal ushort nRegister;
        internal ushort nValue;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal unsafe struct XnSDKVersion
    {
        internal byte nMajor;
        internal byte nMinor;
        internal byte nMaintenance;
        internal ushort nBuild;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal unsafe struct XnCmosBlankingUnits
    {
        internal XnCMOSType nCmosID;
        internal ushort nUnits;
        internal ushort nNumberOfFrames;
    }

    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal unsafe struct XnAHBData
    {
        internal uint nRegister;
        internal uint nValue;
        internal uint nMask;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal unsafe struct XnParamFlashData
    {
        internal uint nOffset;
        internal uint nSize;
        internal byte* pData;
    }

    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal unsafe struct XnEmitterData
    {
        internal ushort m_State;
        internal ushort m_SetPointVoltage;
        internal ushort m_SetPointClocks;
        internal ushort m_PD_Reading;
        internal ushort m_EmitterSet;
        internal ushort m_EmitterSettingLogic;
        internal ushort m_LightMeasureLogic;
        internal ushort m_IsAPCEnabled;
        internal ushort m_EmitterSetStepSize;
        internal ushort m_ApcTolerance;
        internal ushort m_SubClocking;
        internal ushort m_Precision;
    }

    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal unsafe struct XnVersions
    {
        internal byte nMajor;
        internal byte nMinor;
        internal ushort nBuild;
        internal uint nChip;
        internal ushort nFPGA;
        internal ushort nSystemVersion;
        internal XnSDKVersion SDK;
        internal XnHWVer HWVer;
        internal XnFWVer FWVer;
        internal XnSensorVer SensorVer;
        internal XnChipVer ChipVer;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal unsafe struct XnPixelRegistration
    {
        internal uint nDepthX;
        internal uint nDepthY;
        internal ushort nDepthValue;
        internal uint nImageXRes;
        internal uint nImageYRes;
        internal uint nImageX;
        internal uint nImageY;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal unsafe struct XnTecFastConvergenceData
    {
        internal short m_SetPointTemperature;
        internal short m_MeasuredTemperature;
        internal int m_ProportionalError;
        internal int m_IntegralError;
        internal int m_DerivativeError;
        internal ushort m_ScanMode;
        internal ushort m_HeatMode;
        internal ushort m_TecDutyCycle;
        internal ushort m_TemperatureRange;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal unsafe struct XnProjectorFaultData
    {
        internal ushort nMinThreshold;
        internal ushort nMaxThreshold;
        internal int bProjectorFaultEvent;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal unsafe struct XnLedState
    {
        internal ushort nLedID;
        internal ushort nState;
    }

    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal unsafe struct XnBist
    {
        internal uint nTestsMask;
        internal uint nFailures;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal unsafe struct XnI2CReadData
    {
        internal ushort nBus;
        internal ushort nSlaveAddress;
        internal fixed ushort cpReadBuffer [10];
        internal fixed ushort cpWriteBuffer [10];
        internal ushort nReadSize;
        internal ushort nWriteSize;
    }

    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    internal unsafe struct XnI2CWriteData
    {
        internal ushort nBus;
        internal ushort nSlaveAddress;
        internal fixed ushort cpWriteBuffer [10];
        internal ushort nWriteSize;
    }
    
    internal enum XnFirmwareCroppingMode
    {
        XN_FIRMWARE_CROPPING_MODE_DISABLED = 0,
        XN_FIRMWARE_CROPPING_MODE_NORMAL = 1,
        XN_FIRMWARE_CROPPING_MODE_INCREASED_FPS = 2,
    }

    internal enum XnCroppingMode
    {
        XN_CROPPING_MODE_NORMAL = 1,
        XN_CROPPING_MODE_INCREASED_FPS = 2,
        XN_CROPPING_MODE_SOFTWARE_ONLY = 3,
    }

    internal enum XnImageCMOSType
    {
        XN_IMAGE_CMOS_NONE = 0,
        XN_IMAGE_CMOS_MT9M112 = 1,
        XN_IMAGE_CMOS_MT9D131 = 2,
        XN_IMAGE_CMOS_MT9M114 = 3,
    }
    
    internal enum XnDepthCMOSType
    {
        XN_DEPTH_CMOS_NONE = 0,
        XN_DEPTH_CMOS_MT9M001 = 1,
        XN_DEPTH_CMOS_AR130 = 2,
    }

    internal enum XnProcessingType
    {
        XN_PROCESSING_DONT_CARE = 0,
        XN_PROCESSING_HARDWARE = 1,
        XN_PROCESSING_SOFTWARE = 2,
    }
    
    internal enum XnSensorUsbInterface
    {
        XN_SENSOR_USB_INTERFACE_DEFAULT = 0,
        XN_SENSOR_USB_INTERFACE_ISO_ENDPOINTS = 1,
        XN_SENSOR_USB_INTERFACE_BULK_ENDPOINTS = 2,
        XN_SENSOR_USB_INTERFACE_ISO_ENDPOINTS_LOW_DEPTH = 3,
    }
    
    internal enum XnBistType
    {
        XN_BIST_IMAGE_CMOS = 1,
        XN_BIST_IR_CMOS = 2,
        XN_BIST_POTENTIOMETER = 4,
        XN_BIST_FLASH = 8,
        XN_BIST_FULL_FLASH = 16,
        XN_BIST_PROJECTOR_TEST_MASK = 32,
        XN_BIST_TEC_TEST_MASK = 64,
        XN_BIST_NESA_TEST_MASK = 128,
        XN_BIST_NESA_UNLIMITED_TEST_MASK = 256,
        XN_BIST_ALL = -385,
    }
    
    internal enum XnBistError
    {
        XN_BIST_RAM_TEST_FAILURE = 1,
        XN_BIST_IR_CMOS_CONTROL_BUS_FAILURE = 2,
        XN_BIST_IR_CMOS_DATA_BUS_FAILURE = 4,
        XN_BIST_IR_CMOS_BAD_VERSION = 8,
        XN_BIST_IR_CMOS_RESET_FAILUE = 16,
        XN_BIST_IR_CMOS_TRIGGER_FAILURE = 32,
        XN_BIST_IR_CMOS_STROBE_FAILURE = 64,
        XN_BIST_COLOR_CMOS_CONTROL_BUS_FAILURE = 128,
        XN_BIST_COLOR_CMOS_DATA_BUS_FAILURE = 256,
        XN_BIST_COLOR_CMOS_BAD_VERSION = 512,
        XN_BIST_COLOR_CMOS_RESET_FAILUE = 1024,
        XN_BIST_FLASH_WRITE_LINE_FAILURE = 2048,
        XN_BIST_FLASH_TEST_FAILURE = 4096,
        XN_BIST_POTENTIOMETER_CONTROL_BUS_FAILURE = 8192,
        XN_BIST_POTENTIOMETER_FAILURE = 16384,
        XN_BIST_AUDIO_TEST_FAILURE = 32768,
        XN_BIST_PROJECTOR_TEST_LD_FAIL = 65536,
        XN_BIST_PROJECTOR_TEST_LD_FAILSAFE_TRIG_FAIL = 131072,
        XN_BIST_PROJECTOR_TEST_FAILSAFE_HIGH_FAIL = 262144,
        XN_BIST_PROJECTOR_TEST_FAILSAFE_LOW_FAIL = 524288,
        XN_TEC_TEST_HEATER_CROSSED = 1048576,
        XN_TEC_TEST_HEATER_DISCONNETED = 2097152,
        XN_TEC_TEST_TEC_CROSSED = 4194304,
        XN_TEC_TEST_TEC_FAULT = 8388608,
    }
    
    internal static unsafe class PS1080
    {
        internal const int XN_FIRMWARE_CROPPING_MODE_DISABLED = 0;
        internal const int XN_FIRMWARE_CROPPING_MODE_NORMAL = 1;
        internal const int XN_FIRMWARE_CROPPING_MODE_INCREASED_FPS = 2;
        
        internal const int ONI_TIMEOUT_NONE = 0;
        internal const int ONI_TIMEOUT_FOREVER = -1;
        
        internal const int XN_MODULE_PROPERTY_USB_INTERFACE = 276885505;
        internal const int XN_MODULE_PROPERTY_MIRROR = 276885506;
        internal const int XN_MODULE_PROPERTY_RESET_SENSOR_ON_STARTUP = 276885508;
        internal const int XN_MODULE_PROPERTY_LEAN_INIT = 276885509;
        internal const int XN_MODULE_PROPERTY_SERIAL_NUMBER = 276885510;
        internal const int XN_MODULE_PROPERTY_VERSION = 276885511;
        internal const int XN_MODULE_PROPERTY_FIRMWARE_FRAME_SYNC = 276885512;
        internal const int XN_MODULE_PROPERTY_HOST_TIMESTAMPS = 276889463;
        internal const int XN_MODULE_PROPERTY_CLOSE_STREAMS_ON_SHUTDOWN = 276889464;
        internal const int XN_MODULE_PROPERTY_FIRMWARE_LOG_INTERVAL = 276889471;
        internal const int XN_MODULE_PROPERTY_PRINT_FIRMWARE_LOG = 276889472;
        internal const int XN_MODULE_PROPERTY_FIRMWARE_LOG_FILTER = 276889473;
        internal const int XN_MODULE_PROPERTY_FIRMWARE_LOG = 276889474;
        internal const int XN_MODULE_PROPERTY_FIRMWARE_CPU_INTERVAL = 276889475;
        internal const int XN_MODULE_PROPERTY_PHYSICAL_DEVICE_NAME = 276889466;
        internal const int XN_MODULE_PROPERTY_VENDOR_SPECIFIC_DATA = 276889467;
        internal const int XN_MODULE_PROPERTY_SENSOR_PLATFORM_STRING = 276889468;
        internal const int XN_MODULE_PROPERTY_FIRMWARE_PARAM = 276881409;
        internal const int XN_MODULE_PROPERTY_RESET = 276881410;
        internal const int XN_MODULE_PROPERTY_IMAGE_CONTROL = 276881411;
        internal const int XN_MODULE_PROPERTY_DEPTH_CONTROL = 276881412;
        internal const int XN_MODULE_PROPERTY_AHB = 276881413;
        internal const int XN_MODULE_PROPERTY_LED_STATE = 276881414;
        internal const int XN_MODULE_PROPERTY_EMITTER_STATE = 276881415;
        internal const int XN_MODULE_PROPERTY_CMOS_BLANKING_UNITS = 276889460;
        internal const int XN_MODULE_PROPERTY_CMOS_BLANKING_TIME = 276889461;
        internal const int XN_MODULE_PROPERTY_FILE_LIST = 276889476;
        internal const int XN_MODULE_PROPERTY_FLASH_CHUNK = 276889477;
        internal const int XN_MODULE_PROPERTY_FILE = 276889478;
        internal const int XN_MODULE_PROPERTY_DELETE_FILE = 276889479;
        internal const int XN_MODULE_PROPERTY_FILE_ATTRIBUTES = 276889480;
        internal const int XN_MODULE_PROPERTY_TEC_SET_POINT = 276889481;
        internal const int XN_MODULE_PROPERTY_TEC_STATUS = 276889482;
        internal const int XN_MODULE_PROPERTY_TEC_FAST_CONVERGENCE_STATUS = 276889483;
        internal const int XN_MODULE_PROPERTY_EMITTER_SET_POINT = 276889484;
        internal const int XN_MODULE_PROPERTY_EMITTER_STATUS = 276889485;
        internal const int XN_MODULE_PROPERTY_I2C = 276889486;
        internal const int XN_MODULE_PROPERTY_BIST = 276889487;
        internal const int XN_MODULE_PROPERTY_PROJECTOR_FAULT = 276889488;
        internal const int XN_MODULE_PROPERTY_APC_ENABLED = 276889489;
        internal const int XN_MODULE_PROPERTY_FIRMWARE_TEC_DEBUG_PRINT = 276889490;
        internal const int XN_STREAM_PROPERTY_INPUT_FORMAT = 276824065;
        internal const int XN_STREAM_PROPERTY_CROPPING_MODE = 276824066;
        internal const int XN_STREAM_PROPERTY_CLOSE_RANGE = 276885507;
        internal const int XN_STREAM_PROPERTY_PIXEL_REGISTRATION = 276828161;
        internal const int XN_STREAM_PROPERTY_WHITE_BALANCE_ENABLED = 276828162;
        internal const int XN_STREAM_PROPERTY_GAIN = 276828163;
        internal const int XN_STREAM_PROPERTY_HOLE_FILTER = 276828164;
        internal const int XN_STREAM_PROPERTY_REGISTRATION_TYPE = 276828165;
        internal const int XN_STREAM_PROPERTY_AGC_BIN = 276828166;
        internal const int XN_STREAM_PROPERTY_CONST_SHIFT = 276828167;
        internal const int XN_STREAM_PROPERTY_PIXEL_SIZE_FACTOR = 276828168;
        internal const int XN_STREAM_PROPERTY_MAX_SHIFT = 276828169;
        internal const int XN_STREAM_PROPERTY_PARAM_COEFF = 276828170;
        internal const int XN_STREAM_PROPERTY_SHIFT_SCALE = 276828171;
        internal const int XN_STREAM_PROPERTY_ZERO_PLANE_DISTANCE = 276828172;
        internal const int XN_STREAM_PROPERTY_ZERO_PLANE_PIXEL_SIZE = 276828173;
        internal const int XN_STREAM_PROPERTY_EMITTER_DCMOS_DISTANCE = 276828174;
        internal const int XN_STREAM_PROPERTY_DCMOS_RCMOS_DISTANCE = 276828175;
        internal const int XN_STREAM_PROPERTY_S2D_TABLE = 276828176;
        internal const int XN_STREAM_PROPERTY_D2S_TABLE = 276828177;
        internal const int XN_STREAM_PROPERTY_DEPTH_SENSOR_CALIBRATION_INFO = 276828178;
        internal const int XN_STREAM_PROPERTY_GMC_MODE = 276889412;
        internal const int XN_STREAM_PROPERTY_GMC_DEBUG = 276889413;
        internal const int XN_STREAM_PROPERTY_WAVELENGTH_CORRECTION = 276889414;
        internal const int XN_STREAM_PROPERTY_WAVELENGTH_CORRECTION_DEBUG = 276889415;
        internal const int XN_STREAM_PROPERTY_FLICKER = 276832257;
        
        internal const int XN_CROPPING_MODE_NORMAL = 1;
        internal const int XN_CROPPING_MODE_INCREASED_FPS = 2;
        internal const int XN_CROPPING_MODE_SOFTWARE_ONLY = 3;
        
        internal const int XN_ERROR_STATE_OK = 0;
        internal const int XN_ERROR_STATE_DEVICE_PROJECTOR_FAULT = 1;
        internal const int XN_ERROR_STATE_DEVICE_OVERHEAT = 2;
        
        internal const int XN_IMAGE_CMOS_NONE = 0;
        internal const int XN_IMAGE_CMOS_MT9M112 = 1;
        internal const int XN_IMAGE_CMOS_MT9D131 = 2;
        internal const int XN_IMAGE_CMOS_MT9M114 = 3;
        
        internal const int XN_DEPTH_CMOS_NONE = 0;
        internal const int XN_DEPTH_CMOS_MT9M001 = 1;
        internal const int XN_DEPTH_CMOS_AR130 = 2;
        
        internal const int XN_PROCESSING_DONT_CARE = 0;
        internal const int XN_PROCESSING_HARDWARE = 1;
        internal const int XN_PROCESSING_SOFTWARE = 2;
        
        internal const int XN_SENSOR_USB_INTERFACE_DEFAULT = 0;
        internal const int XN_SENSOR_USB_INTERFACE_ISO_ENDPOINTS = 1;
        internal const int XN_SENSOR_USB_INTERFACE_BULK_ENDPOINTS = 2;
        internal const int XN_SENSOR_USB_INTERFACE_ISO_ENDPOINTS_LOW_DEPTH = 3;
        
        internal const int XN_BIST_IMAGE_CMOS = 1;
        internal const int XN_BIST_IR_CMOS = 2;
        internal const int XN_BIST_POTENTIOMETER = 4;
        internal const int XN_BIST_FLASH = 8;
        internal const int XN_BIST_FULL_FLASH = 16;
        internal const int XN_BIST_PROJECTOR_TEST_MASK = 32;
        internal const int XN_BIST_TEC_TEST_MASK = 64;
        internal const int XN_BIST_NESA_TEST_MASK = 128;
        internal const int XN_BIST_NESA_UNLIMITED_TEST_MASK = 256;
        internal const int XN_BIST_ALL = -385;
        
        internal const int XN_BIST_RAM_TEST_FAILURE = 1;
        internal const int XN_BIST_IR_CMOS_CONTROL_BUS_FAILURE = 2;
        internal const int XN_BIST_IR_CMOS_DATA_BUS_FAILURE = 4;
        internal const int XN_BIST_IR_CMOS_BAD_VERSION = 8;
        internal const int XN_BIST_IR_CMOS_RESET_FAILUE = 16;
        internal const int XN_BIST_IR_CMOS_TRIGGER_FAILURE = 32;
        internal const int XN_BIST_IR_CMOS_STROBE_FAILURE = 64;
        internal const int XN_BIST_COLOR_CMOS_CONTROL_BUS_FAILURE = 128;
        internal const int XN_BIST_COLOR_CMOS_DATA_BUS_FAILURE = 256;
        internal const int XN_BIST_COLOR_CMOS_BAD_VERSION = 512;
        internal const int XN_BIST_COLOR_CMOS_RESET_FAILUE = 1024;
        internal const int XN_BIST_FLASH_WRITE_LINE_FAILURE = 2048;
        internal const int XN_BIST_FLASH_TEST_FAILURE = 4096;
        internal const int XN_BIST_POTENTIOMETER_CONTROL_BUS_FAILURE = 8192;
        internal const int XN_BIST_POTENTIOMETER_FAILURE = 16384;
        internal const int XN_BIST_AUDIO_TEST_FAILURE = 32768;
        internal const int XN_BIST_PROJECTOR_TEST_LD_FAIL = 65536;
        internal const int XN_BIST_PROJECTOR_TEST_LD_FAILSAFE_TRIG_FAIL = 131072;
        internal const int XN_BIST_PROJECTOR_TEST_FAILSAFE_HIGH_FAIL = 262144;
        internal const int XN_BIST_PROJECTOR_TEST_FAILSAFE_LOW_FAIL = 524288;
        internal const int XN_TEC_TEST_HEATER_CROSSED = 1048576;
        internal const int XN_TEC_TEST_HEATER_DISCONNETED = 2097152;
        internal const int XN_TEC_TEST_TEC_CROSSED = 4194304;
        internal const int XN_TEC_TEST_TEC_FAULT = 8388608;
        
    }
}
