namespace OpenNI2
{

    public unsafe static class CoordinateConverter
    {
        public static void DepthToWorld(SensorStream depthStream, float depthX, float depthY, float depthZ, ref float worldX, ref float worldY, ref float worldZ)
        {
            fixed(float* pWorldX = &worldX)
            fixed(float* pWorldY = &worldY)
            fixed(float* pWorldZ = &worldZ)
                OniCAPI.oniCoordinateConverterDepthToWorld(depthStream._pStream, depthX, depthY, depthZ, pWorldX, pWorldY, pWorldZ).ThrowExectionIfStatusIsNotOk();
        }

        public static void DepthToColor(SensorStream depthStream, SensorStream colorStream, int depthX, int depthY, ushort depthZ, ref int colorX, ref int colorY)
        {
            fixed(int* pColorX = &colorX)
            fixed(int* pColorY = &colorY)
                OniCAPI.oniCoordinateConverterDepthToColor(depthStream._pStream, colorStream._pStream, depthX, depthY, depthZ, pColorX, pColorY).ThrowExectionIfStatusIsNotOk();
        }

        public static void WorldToDepth(SensorStream depthStream, float worldX, float worldY, float worldZ, ref float depthX, ref float depthY, ref float depthZ)
        {
            fixed(float* pDepthX = &depthX)
            fixed(float* pDepthY = &depthY)
            fixed(float* pDepthZ = &depthZ)
                OniCAPI.oniCoordinateConverterWorldToDepth(depthStream._pStream, worldX, worldY, worldZ, pDepthX, pDepthY, pDepthZ).ThrowExectionIfStatusIsNotOk();
        }
    }
}