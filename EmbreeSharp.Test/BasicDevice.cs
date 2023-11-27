using EmbreeSharp.Native;

namespace EmbreeSharp.Test;

[TestClass]
public class BasicDevice
{
    [TestMethod]
    public unsafe void StructSize()
    {
        RTCDevice device = GlobalFunctions.rtcNewDevice(null);
        GlobalFunctions.rtcReleaseDevice(device);
    }
}
