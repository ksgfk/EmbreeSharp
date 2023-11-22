using EmbreeSharp.Native;

namespace EmbreeSharp.Test;

[TestClass]
public class EmbreeStructLayout
{
    [TestMethod]
    public unsafe void StructSize()
    {
        Assert.AreEqual(sizeof(RTCBounds), 32);
        Assert.AreEqual(sizeof(RTCLinearBounds), 64);
        Assert.AreEqual(sizeof(RTCPointQuery), 32);
        Assert.AreEqual(sizeof(RTCPointQuery4), 80);
        Assert.AreEqual(sizeof(RTCPointQuery8), 160);
        Assert.AreEqual(sizeof(RTCPointQuery16), 320);
        Assert.AreEqual(sizeof(RTCPointQueryContext), 144);
        Assert.AreEqual(sizeof(RTCPointQueryFunctionArguments), 48);
    }
}
